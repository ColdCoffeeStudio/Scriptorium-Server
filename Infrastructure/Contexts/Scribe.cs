using System;
using System.Collections.Generic;

namespace Infrastructure.Contexts;

public partial class Scribe
{
    public string id { get; set; } = null!;

    public string username { get; set; } = null!;

    public virtual ICollection<Encyclopedium> Encyclopedia { get; set; } = new List<Encyclopedium>();
}
