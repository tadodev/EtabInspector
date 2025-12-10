using EtabInspector.UI.ViewModels;

namespace EtabInspector.UI.Contracts.Services;

public interface IDocumentManagerService
{
    event EventHandler<DocumentViewModel>? DocumentAdded;
    event EventHandler<DocumentViewModel>? DocumentRemoved;
    event EventHandler<DocumentViewModel>? ActiveDocumentChanged;

    IReadOnlyList<DocumentViewModel> Documents { get; }
    DocumentViewModel? ActiveDocument { get; set; }

    void AddDocument(DocumentViewModel document);
    void RemoveDocument(DocumentViewModel document);
    void CloseDocument(DocumentViewModel document);
    void CloseAllDocuments();
}
