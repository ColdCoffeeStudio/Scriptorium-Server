using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Annotation: IEntity<Annotation>
{
    public int Id { get; }
    public string Title { get; }
    public int StartPage { get; }
    public int EndPage { get; }
    public string ContentUrl { get; }
    public DateOnly Date { get; }
    public Theme Theme { get; }
    public Encyclopedia Encyclopedia { get; }

    public static Result<Annotation> Create(int id, string title, int startPage, int endPage, string contentUrl, DateOnly date, 
        Theme theme, Encyclopedia encyclopedia)
    {
        AnnotationErrors errors = new AnnotationErrors();
        Result<Annotation> result;

        if (id <= 0)
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.MissingId(), false);
        }
        else if (String.IsNullOrEmpty(title))
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.MissingTitle(), false);
        }
        else if (startPage < 0)
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.NegativeStartPage(), false);
        }
        else if (endPage < 0)
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.NegativeEndPage(), false);
        }
        else if (endPage < startPage)
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.InvalidEndPage(startPage, endPage), false);
        }
        else if (date == DateOnly.MinValue || date > DateOnly.FromDateTime(DateTime.Now))
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.MissingDate(), false);
        }
        else if (theme.Equals(Theme.Empty()))
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.MissingTheme(), false);
        }
        else if (encyclopedia.Equals(Encyclopedia.Empty()))
        {
            result = new Result<Annotation>(Annotation.Empty(), errors.MissingEncyclopedia(), false);
        }
        else
        {
            contentUrl = String.IsNullOrEmpty(contentUrl)
                ? Constants.NotImplementedContentUrl
                : contentUrl;
            
            result = new Result<Annotation>(
                new Annotation(id, title, startPage, endPage, contentUrl, date, theme, encyclopedia),
                Error.Empty(), true);
        }
        
        return result;
    }
        
    public static Annotation Empty()
    {
        return new Annotation(-1, "", -1,-1, "", DateOnly.MinValue, Theme.Empty(), Encyclopedia.Empty());
    }

    private Annotation(int id, string title, int startPage, int endPage, string contentUrl, DateOnly date, Theme theme,
        Encyclopedia encyclopedia)
    {
        Id = id;
        Title = title;
        StartPage = startPage;
        EndPage = endPage;
        ContentUrl = contentUrl;
        Date = date;
        Theme = theme;
        Encyclopedia = encyclopedia;
    }
}