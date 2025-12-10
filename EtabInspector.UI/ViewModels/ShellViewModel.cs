using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EtabInspector.UI.Contracts.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EtabInspector.UI.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly IDocumentManagerService documentManager;
    private bool isSubscribedToDocumentManager = false;

    // Documents
    [ObservableProperty]
    private ObservableCollection<DocumentViewModel> documents = new();

    [ObservableProperty]
    private DocumentViewModel? activeDocument;

    // Tool Windows
    [ObservableProperty]
    private ExplorerViewModel explorer;

    [ObservableProperty]
    private PropertiesViewModel properties;

    [ObservableProperty]
    private OutputViewModel output;

    // Commands
    public ICommand NewModelCommand { get; }
    public ICommand NewDrawingCommand { get; }
    public ICommand CloseDocumentCommand { get; }
    public ICommand CloseAllDocumentsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand LoadedCommand { get; }

    // View commands
    public ICommand ToggleExplorerCommand { get; }
    public ICommand TogglePropertiesCommand { get; }
    public ICommand ToggleOutputCommand { get; }

    public ShellViewModel(IDocumentManagerService documentManager)
    {
        this.documentManager = documentManager;

        // Initialize tool windows
        explorer = new ExplorerViewModel();
        properties = new PropertiesViewModel();
        output = new OutputViewModel();

        // Document commands
        NewModelCommand = new RelayCommand(OnNewModel);
        NewDrawingCommand = new RelayCommand(OnNewDrawing);
        CloseDocumentCommand = new RelayCommand(OnCloseDocument, CanCloseDocument);
        CloseAllDocumentsCommand = new RelayCommand(OnCloseAllDocuments, () => Documents.Any());
        ExitCommand = new RelayCommand(OnExit);
        LoadedCommand = new RelayCommand(OnLoaded);

        // View commands
        ToggleExplorerCommand = new RelayCommand(() => Explorer.IsVisible = !Explorer.IsVisible);
        TogglePropertiesCommand = new RelayCommand(() => Properties.IsVisible = !Properties.IsVisible);
        ToggleOutputCommand = new RelayCommand(() => Output.IsVisible = !Output.IsVisible);

        // Subscribe to document manager events
        SubscribeToDocumentManager();
    }

    private void SubscribeToDocumentManager()
    {
        if (!isSubscribedToDocumentManager)
        {
            documentManager.DocumentAdded += OnDocumentAdded;
            documentManager.DocumentRemoved += OnDocumentRemoved;
            documentManager.ActiveDocumentChanged += OnActiveDocumentChanged;
            isSubscribedToDocumentManager = true;
        }
    }

    private void UnsubscribeFromDocumentManager()
    {
        if (isSubscribedToDocumentManager)
        {
            documentManager.DocumentAdded -= OnDocumentAdded;
            documentManager.DocumentRemoved -= OnDocumentRemoved;
            documentManager.ActiveDocumentChanged -= OnActiveDocumentChanged;
            isSubscribedToDocumentManager = false;
        }
    }

    private void OnLoaded()
    {
        SubscribeToDocumentManager();
        Output.AddLog("Application initialized");
    }

    public void Shutdown()
    {
        UnsubscribeFromDocumentManager();
        Output.AddLog("Application shutting down");
    }

    private void OnNewModel()
    {
        var modelDoc = new ModelDocumentViewModel
        {
            Title = $"Model {Documents.Count(d => d is ModelDocumentViewModel) + 1}"
        };
        documentManager.AddDocument(modelDoc);
        Output.AddLog($"Created new model: {modelDoc.Title}");
    }

    private void OnNewDrawing()
    {
        var drawingDoc = new DrawingDocumentViewModel
        {
            Title = $"Drawing {Documents.Count(d => d is DrawingDocumentViewModel) + 1}"
        };
        documentManager.AddDocument(drawingDoc);
        Output.AddLog($"Created new drawing: {drawingDoc.Title}");
    }

    private bool CanCloseDocument()
        => ActiveDocument != null && ActiveDocument.CanClose;

    private void OnCloseDocument()
    {
        if (ActiveDocument != null)
        {
            Output.AddLog($"Closing document: {ActiveDocument.Title}");
            documentManager.CloseDocument(ActiveDocument);
        }
    }

    private void OnCloseAllDocuments()
    {
        Output.AddLog("Closing all documents");
        documentManager.CloseAllDocuments();
    }

    private void OnExit()
    {
        Application.Current.Shutdown();
    }

    private void OnDocumentAdded(object? sender, DocumentViewModel document)
    {
        Documents.Add(document);
        ((RelayCommand)CloseDocumentCommand).NotifyCanExecuteChanged();
        ((RelayCommand)CloseAllDocumentsCommand).NotifyCanExecuteChanged();
    }

    private void OnDocumentRemoved(object? sender, DocumentViewModel document)
    {
        Documents.Remove(document);
        ((RelayCommand)CloseDocumentCommand).NotifyCanExecuteChanged();
        ((RelayCommand)CloseAllDocumentsCommand).NotifyCanExecuteChanged();
    }

    private void OnActiveDocumentChanged(object? sender, DocumentViewModel document)
    {
        ActiveDocument = document;
    }

    partial void OnActiveDocumentChanged(DocumentViewModel? value)
    {
        if (documentManager.ActiveDocument != value)
        {
            documentManager.ActiveDocument = value;
        }
        ((RelayCommand)CloseDocumentCommand).NotifyCanExecuteChanged();
    }
}
