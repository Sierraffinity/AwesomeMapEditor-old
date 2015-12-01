using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    /// <summary>
    /// Code priorities
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Defauls priority
        /// </summary>
        none,
        /// <summary>
        /// Priority for main codes
        /// </summary>
        main,
        /// <summary>
        /// Unused
        /// </summary>
        high,
        /// <summary>
        /// For general, non-specific codes
        /// </summary>
        low,
        /// <summary>
        /// For pointer lists
        /// </summary>
        pointer,
        /// <summary>
        /// For unit data
        /// </summary>
        unit,
        /// <summary>
        /// For move manuals used by movement codes
        /// </summary>
        moveManual,
        /// <summary>
        /// For shops item lists
        /// </summary>
        shopList,
        /// <summary>
        /// For ballista data
        /// </summary>
        ballista,
        /// <summary>
        /// For assembly language
        /// </summary>
        ASM,
        /// <summary>
        /// For battle data used by fighting codes
        /// </summary>
        battleData,
        /// <summary>
        /// For reinforcement data used by unit data
        /// </summary>
        reinforcementData,
        /// <summary>
        /// 
        /// </summary>
        coordList,
        /// <summary>
        /// Unknown priority
        /// </summary>
        unknown
    }
}
