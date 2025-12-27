using System;
using System.Collections.Generic;

namespace Infrastructure.Contexts;

public partial class Annotation
{
    public int id { get; set; }

    public string title { get; set; } = null!;

    public int startPage { get; set; }

    public int endPage { get; set; }

    public string contentUrl { get; set; } = null!;

    public string tags { get; set; } = null!;

    public DateOnly date { get; set; }

    public int themeId { get; set; }

    public int encyclopediaId { get; set; }

    public virtual Encyclopedium encyclopedia { get; set; } = null!;

    public virtual Theme theme { get; set; } = null!;
}
