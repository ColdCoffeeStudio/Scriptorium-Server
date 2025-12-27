using Domain.Shared;

namespace Domain.Entities;

public class AnnotationList: IEntity<AnnotationList>
{
    public Guid Id { get; }
    public List<Annotation> Annotations { get; }

    public static Result<AnnotationList> Create(List<Annotation> annotations)
    {
        return new Result<AnnotationList>(new AnnotationList(Guid.NewGuid(), annotations), Error.Empty(), true);
    }

    public static AnnotationList Empty()
    {
        return new AnnotationList(Guid.Empty, new List<Annotation>());
    }

    private AnnotationList(Guid id, List<Annotation> annotations)
    {
        Id = id;
        Annotations = annotations;
    }
}