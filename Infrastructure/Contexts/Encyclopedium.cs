using System;
using System.Collections.Generic;

namespace Infrastructure.Contexts;

public partial class Encyclopedium
{
    public int id { get; set; }

    public string title { get; set; } = null!;

    public string scribeId { get; set; } = null!;

    public virtual ICollection<Annotation> Annotations { get; set; } = new List<Annotation>();

    public virtual Scribe scribe { get; set; } = null!;
}
