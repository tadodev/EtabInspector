using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EtabInspector.UI.Contracts.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EtabInspector.UI.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly IDocumentManagerService _documentManager;
    private bool _isSubscribedToDocumentManager = false;

    [ObservableProperty]
    private ObservableCollection<DocumentViewModel> _documents = new();

    [ObservableProperty]
    private DocumentViewModel? _activeDocument;

    public ICommand NewModelCommand { get; }
    public ICommand NewDrawingCommand { get; }
    public ICommand CloseDocumentCommand { get; }
    public ICommand CloseAllDocumentsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand LoadedCommand { get; }

    public ShellViewModel(IDocumentManagerService documentManager)
    {
        _documentManager = documentManager;

        NewModelCommand = new RelayCommand(OnNewModel);
        NewDrawingCommand = new RelayCommand(OnNewDrawing);
        CloseDocumentCommand = new RelayCommand(OnCloseDocument, CanCloseDocument);
        CloseAllDocumentsCommand = new RelayCommand(OnCloseAllDocuments, () => Documents.Any());
        ExitCommand = new RelayCommand(OnExit);
        LoadedCommand = new RelayCommand(OnLoaded);

        // Subscribe to document manager events
        SubscribeToDocumentManager();
    }

    private void SubscribeToDocumentManager()
    {
        if (!_isSubscribedToDocumentManager)
        {
            _documentManager.DocumentAdded += OnDocumentAdded;
            _documentManager.DocumentRemoved += OnDocumentRemoved;
            _documentManager.ActiveDocumentChanged += OnActiveDocumentChanged;
            _isSubscribedToDocumentManager = true;
        }
    }

    private void UnsubscribeFromDocumentManager()
    {
        if (_isSubscribedToDocumentManager)
        {
            _documentManager.DocumentAdded -= OnDocumentAdded;
            _documentManager.DocumentRemoved -= OnDocumentRemoved;
            _documentManager.ActiveDocumentChanged -= OnActiveDocumentChanged;
            _isSubscribedToDocumentManager = false;
        }
    }

    private void OnLoaded()
    {
        // Re-subscribe in case we got unloaded
        SubscribeToDocumentManager();
    }

    public void Shutdown()
    {
        // Call this method from the window close/shutdown handlers
        UnsubscribeFromDocumentManager();
    }

    private void OnNewModel()
    {
        var modelDoc = new ModelDocumentViewModel
        {
            Title = $"Model {Documents.Count(d => d is ModelDocumentViewModel) + 1}"
        };
        _documentManager.AddDocument(modelDoc);
    }

    private void OnNewDrawing()
    {
        var drawingDoc = new DrawingDocumentViewModel
        {
            Title = $"Drawing {Documents.Count(d => d is DrawingDocumentViewModel) + 1}"
        };
        _documentManager.AddDocument(drawingDoc);
    }

    private bool CanCloseDocument()
        => ActiveDocument != null && ActiveDocument.CanClose;

    private void OnCloseDocument()
    {
        if (ActiveDocument != null)
        {
            _documentManager.CloseDocument(ActiveDocument);
        }
    }

    private void OnCloseAllDocuments()
    {
        _documentManager.CloseAllDocuments();
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
        if (_documentManager.ActiveDocument != value)
        {
            _documentManager.ActiveDocument = value;
        }
        ((RelayCommand)CloseDocumentCommand).NotifyCanExecuteChanged();
    }
}
