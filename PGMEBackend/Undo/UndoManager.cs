using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace PGMEBackend
{
    public class UndoStep
    {
        public bool firstStep = false;

        public UndoStep()
        {
            if (Program.isEdited == false)
            {
                Program.isEdited = true;
                firstStep = true;
            }
        }

        public UndoStep(Action undo, Action redo)
        {
            if (Program.isEdited == false)
            {
                Program.isEdited = true;
                firstStep = true;
            }

            Undo = undo;
            Redo = redo;
        }

        public Action Undo;
        public Action Redo;

        public virtual void CallUndo()
        {
            if (Undo != null)
            {
                if (firstStep)
                    Program.isEdited = false;
                Undo.Invoke();
            }
        }

        public virtual void CallRedo()
        {
            if (Redo != null)
            {
                if (firstStep)
                    Program.isEdited = true;
                Redo.Invoke();
            }
        }
    }

    public class UndoManager
    {
        public class UndoRedoEventArgs : EventArgs {
            public UndoStep Step;
        }
        private static Stack<UndoStep> undoStack = new Stack<UndoStep>();
        private static Stack<UndoStep> redoStack = new Stack<UndoStep>();

        public static event EventHandler<UndoRedoEventArgs> OnModified;
        public static event EventHandler<UndoRedoEventArgs> OnUndo;
        public static event EventHandler<UndoRedoEventArgs> OnRedo;

        public static bool HasUndo {
            get {
                return undoStack.Count > 0;
            }
        }

        public static bool HasRedo
        {
            get
            {
                return redoStack.Count > 0;
            }
        }

        public static void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
            if (OnModified != null)
                OnModified.Invoke(null, new UndoRedoEventArgs { });
        }

        public static void Add(Action redo, Action undo)
        {
            Add(new UndoStep(undo, redo));
        }

        public static void Add(UndoStep node)
        {
            Add(node, true);
        }
        
        public static void Add(UndoStep node, bool callRedo)
        {
            if(callRedo)
                node.CallRedo();
            undoStack.Push(node);
            redoStack.Clear();
            if (OnModified != null)
                OnModified.Invoke(null, new UndoRedoEventArgs { Step = node });
        }

        public static void Undo()
        {
            if (undoStack.Count > 0)
            {
                var un = undoStack.Pop();
                un.CallUndo();
                redoStack.Push(un);
                if (OnModified != null)
                    OnModified.Invoke(null, new UndoRedoEventArgs { Step = un });
                if (OnUndo != null)
                    OnUndo.Invoke(null, new UndoRedoEventArgs { Step = un });
            }
        }

        public static void Redo()
        {
            if (redoStack.Count > 0)
            {
                var un = redoStack.Pop();
                un.CallRedo();
                undoStack.Push(un);
                if (OnModified != null)
                    OnModified.Invoke(null, new UndoRedoEventArgs { Step = un });
                if (OnRedo!=null)
                    OnRedo.Invoke(null, new UndoRedoEventArgs { Step = un });
            }
        }
    }
}
