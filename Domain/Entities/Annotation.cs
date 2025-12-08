using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Annotation: IEntity<Annotation>
{
    private Guid Id { get; }
    private string Title { get; }
    private int StartPage { get; }
    private int EndPage { get; }
    private string ContentUrl { get; }
    private DateOnly Date { get; }
    private Theme Theme { get; }
    private Encyclopedia Encyclopedia { get; }

    public static Result<Annotation> Create(string title, int startPage, int endPage, string contentUrl, DateOnly date, 
        Theme theme, Encyclopedia encyclopedia)
    {
        AnnotationErrors errors = new AnnotationErrors();
        Result<Annotation> result;

        if (String.IsNullOrEmpty(title))
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
            if (String.IsNullOrEmpty(contentUrl))
            {
                contentUrl = Constants.NotImplementedContentUrl;
            }
            
            result = new Result<Annotation>(
                new Annotation(Guid.NewGuid(), title, startPage, endPage, contentUrl, date, theme, encyclopedia),
                Error.Empty(), true);
        }
        
        return result;
    }
        
    public static Annotation Empty()
    {
        return new Annotation(Guid.Empty, "", -1,-1, "", DateOnly.MinValue, Theme.Empty(), Encyclopedia.Empty());
    }

    private Annotation(Guid id, string title, int startPage, int endPage, string contentUrl, DateOnly date, Theme theme,
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