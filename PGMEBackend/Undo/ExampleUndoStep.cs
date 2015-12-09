using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Undo
{
    public class ExampleUndoStep : UndoStep
    {
        public ExampleUndoStep(float arg)
        {
            Argument = arg;
        }

        public float Argument;
        public override void CallUndo()
        {
            Argument -= 15;
            Console.WriteLine("Undone.  Value now = " + Argument);
        }

        public override void CallRedo()
        {
            Argument += 15;
            Console.WriteLine("Redone.  Value now = " + Argument);
        }
    }

    public class UndoSetProperty : UndoStep {
        public object Object;
        public string PropertyName;
        public object OldValue;
        public object NewValue;
        private System.Reflection.PropertyInfo prop;

        public UndoSetProperty(object obj, string propname, object newValue)
        {
            Object = obj;
            PropertyName = propname;
            prop = Object.GetType().GetProperty(propname);
            NewValue = newValue;
        }

        public override void CallRedo()
        {
            OldValue = prop.GetValue(Object, null);
            prop.SetValue(Object, NewValue, null);
        }

        public override void CallUndo()
        {
            prop.SetValue(Object, OldValue, null);
        }
    }

    public class UndoSetField : UndoStep
    {
        public object Object;
        public string FieldName;
        public object OldValue;
        public object NewValue;
        private System.Reflection.FieldInfo field;

        public UndoSetField(object obj, string fieldname, object newValue)
        {
            Object = obj;
            FieldName = fieldname;
            field = Object.GetType().GetField(fieldname);
            NewValue = newValue;
        }

        public override void CallRedo()
        {
            OldValue = field.GetValue(Object);
            field.SetValue(Object, NewValue);
        }

        public override void CallUndo()
        {
            field.SetValue(Object, OldValue);
        }
    }
}
