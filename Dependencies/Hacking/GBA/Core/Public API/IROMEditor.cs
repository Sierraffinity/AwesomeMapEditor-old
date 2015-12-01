using System.Windows.Forms;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Utility;
using Nintenlord.Feditor.Core.MemoryManagement;

namespace Nintenlord.Feditor.Core.Public_API
{
    /// <summary>
    /// Interface for an editor
    /// </summary>
    public interface IROMEditor : INamed<string>
    {
        /// <summary>
        /// Main editor form off the editor
        /// </summary>
        Form EditorForm { get; }
        /// <summary>
        /// For about/credits screen
        /// </summary>
        string[] CreatorNames { get; }
        /// <summary>
        /// Called when the ROM to edit is to be changed
        /// All operations for the old ROM will still be allowed
        /// during this method's call, but not after that.
        /// </summary>
        /// <param name="memoryManager">New manager of the ROM</param>
        /// <param name="rom">New ROM to edit</param>
        void ChangeROM(IMemoryManager memoryManager, IROM rom);

        /// <summary>
        /// Tells whether this editor supports a perticular game
        /// </summary>
        /// <param name="game">Game to test</param>
        /// <returns>True if game is supported, else false</returns>
        bool SupportGame(GameEnum game);
    }
}
