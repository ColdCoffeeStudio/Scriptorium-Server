using Domain.Shared;

namespace Domain.Errors;

public class ContentTableServiceErrors
{
    public Error AnnotationListFetchingError(int encyclopediaId, Error annotationsListError)
    {
        return new Error("ContentTableServiceErrors.AnnotationListFetchingError", 
            $"An error occured while fetching the annotation list for the encyclopedia id {encyclopediaId} : {annotationsListError.Message}.");
    }

    public Error ContentTableEntryCreationError(Error error)
    {
        return new Error("ContentTableServiceErrors.ContentTableEntryCreationError",
            $"The following error occured while creating one ContentTableEntry : {error.Message}.");
    }
    
    public Error ContentTableCreationError(Error error)
    {
        return new Error("ContentTableServiceErrors.ContentTableCreationError",
            $"The following error occured while creating one ContentTable : {error.Message}.");
    }
}