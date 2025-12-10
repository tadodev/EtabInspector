using System.Collections.ObjectModel;
using EtabInspector.UI.Contracts.Services;
using EtabInspector.UI.ViewModels;

namespace EtabInspector.UI.Services;

public class DocumentManagerService : IDocumentManagerService
{
    private readonly ObservableCollection<DocumentViewModel> _documents = new();
    private DocumentViewModel? _activeDocument;

    public event EventHandler<DocumentViewModel>? DocumentAdded;
    public event EventHandler<DocumentViewModel>? DocumentRemoved;
    public event EventHandler<DocumentViewModel>? ActiveDocumentChanged;

    public IReadOnlyList<DocumentViewModel> Documents => _documents;

    public DocumentViewModel? ActiveDocument
    {
        get => _activeDocument;
        set
        {
            if (_activeDocument != value)
            {
                if (_activeDocument != null)
                {
                    _activeDocument.IsActive = false;
                }

                _activeDocument = value;

                if (_activeDocument != null)
                {
                    _activeDocument.IsActive = true;
                    ActiveDocumentChanged?.Invoke(this, _activeDocument);
                }
            }
        }
    }

    public void AddDocument(DocumentViewModel document)
    {
        if (!_documents.Contains(document))
        {
            _documents.Add(document);
            DocumentAdded?.Invoke(this, document);
            ActiveDocument = document;
        }
        else
        {
            ActiveDocument = document;
        }
    }

    public void RemoveDocument(DocumentViewModel document)
    {
        if (_documents.Contains(document))
        {
            _documents.Remove(document);
            DocumentRemoved?.Invoke(this, document);

            if (ActiveDocument == document)
            {
                ActiveDocument = _documents.LastOrDefault();
            }
        }
    }

    public void CloseDocument(DocumentViewModel document)
    {
        document.OnClose();
        RemoveDocument(document);
    }

    public void CloseAllDocuments()
    {
        var docs = _documents.ToList();
        foreach (var doc in docs)
        {
            CloseDocument(doc);
        }
    }
}
