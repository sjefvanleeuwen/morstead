using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    /// <summary>
    /// These properties are currently not used
    /// They were used by objects that were excluded from the project currently
    /// </summary>
    public class Unused : IUnused
    {
        public IEnumerable<FormElementLabel> Labels { get; set; }
        public IEnumerable<string> Values { get; set; }
        public string ButtonIcon { get; set; }

        public string ButtonText { get; set; }
    }

    public class FormElementLabel
    {
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
