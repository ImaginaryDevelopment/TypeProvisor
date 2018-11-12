using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace CodeGeneration.UI.Controls
{
    class UcBaseTypeDesigner : MyCustomDesigner<UcBaseType>
    {
        UcBaseTypeDesigner() : base(uc => uc.DesignHandle) { }
    }

    // implemented based on https://stackoverflow.com/questions/93541/baseline-snaplines-in-custom-winforms-controls
    class MyCustomDesigner<T> : ControlDesigner
        where T: Control
    {
        readonly Func<T, Control> _getSnapLineComponent;
        public MyCustomDesigner(Func<T,Control> getSnapLineComponent)
        {
            this._getSnapLineComponent = getSnapLineComponent;
        }

        public override IList SnapLines
        {
            get
            {
                /* Code from above */
                IList snapLines = base.SnapLines;

                // *** This will need to be modified to match your user control
                var control = Control as T;
                if (control == null) { return snapLines; }

                // *** This will need to be modified to match the item in your user control
                // This is the control in your UC that you want SnapLines for the entire UC
                var component = this._getSnapLineComponent(control);
                IDesigner designer = TypeDescriptor.CreateDesigner(component, typeof(IDesigner));
                if (designer == null) { return snapLines; }

                // *** This will need to be modified to match the item in your user control
                designer.Initialize(component);

                using (designer)
                {
                    ControlDesigner boxDesigner = designer as ControlDesigner;
                    if (boxDesigner == null) { return snapLines; }

                    foreach (SnapLine line in boxDesigner.SnapLines)
                    {
                        if (line.SnapLineType == SnapLineType.Baseline)
                        {
                            // *** This will need to be modified to match the item in your user control
                            snapLines.Add(new SnapLine(SnapLineType.Baseline,
                                line.Offset + component.Top,
                                line.Filter, line.Priority));
                            break;
                        }
                    }
                }

                return snapLines;
            }

        }
    }
}
