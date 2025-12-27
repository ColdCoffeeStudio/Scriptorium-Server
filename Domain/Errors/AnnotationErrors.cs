using Domain.Shared;

namespace Domain.Errors;

public class AnnotationErrors
{
    public Error MissingId()
    {
        return new Error("Annotation.MissingId", "Annotation id must be greater than 0.");
    }
    
    public Error MissingTitle()
    {
        return new Error("Annotation.MissingTitle", "Annotation title is missing.");
    }

    public Error NegativeStartPage()
    {
        return new Error("Annotation.NegativeStartPage", "Annotation start page must be positive.");
    }

    public Error NegativeEndPage()
    {
        return new Error("Annotation.NegativeEndPage", "Annotation end page must be positive.");
    }

    public Error InvalidEndPage(int startPage, int endPage)
    {
        return new Error("Annotation.InvalidEndPage", $"Annotation end page must be greater than or equal to start page. (Start page: {startPage}; End page: {endPage};)");
    }

    public Error MissingDate()
    {
        return new Error("Annotation.MissingDate", "Annotation date is missing.");
    }

    public Error MissingTheme()
    {
        return new Error("Annotation.MissingTheme", "Annotation theme is missing.");
    }

    public Error MissingEncyclopedia()
    {
        return new Error("Annotation.MissingEncyclopedia", "Annotation encyclopedia is missing.");
    }
}