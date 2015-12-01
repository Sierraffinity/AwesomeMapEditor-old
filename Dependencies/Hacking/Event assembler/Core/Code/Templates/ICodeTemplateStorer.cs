
using System.Collections.Generic;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Event_Assembler.Core.Code.Language.Types;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    public interface ICodeTemplateStorer : IEnumerable<ICodeTemplate>
    {
        void AddCode(ICodeTemplate code, Priority priority);
        CanCauseError<ICodeTemplate> FindTemplate(IInputByteStream reader, IEnumerable<Priority> allowedPriorities);
        CanCauseError<ICodeTemplate> FindTemplate(byte[] code, int index, IEnumerable<Priority> allowedPriorities);
        CanCauseError<ICodeTemplate> FindTemplate(string codeName, Type[] parameterTypes);
        CanCauseError<ICodeTemplate> FindTemplate(string name, Priority priority);
        string[] GetNames();
        bool IsUsedName(string name);
    }
}
