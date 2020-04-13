using Game.IO;
using Game.Exceptions;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Additional;

namespace Game.UI
{
    /// <summary>
    /// This class provides load settings from global object, and set it to
    /// New Game UI
    /// </summary>
    public class SettingsNewGame : MonoBehaviour
    {
        /// <summary>
        /// Reference to settings core
        /// </summary>
        public SettingsCore core;

        /// <summary>
        /// Reference to dropdown of size field of game
        /// </summary>
        public TMP_Dropdown size;
        /// <summary>
        /// Reference to dropdown of difficult
        /// </summary>
        public TMP_Dropdown difficult;

        public AddDDListRaw ddSize;
        public AddDDListRaw ddDifficult;

        /// <summary>
        /// Toggle which define classic mode
        /// </summary>
        public Toggle toggleModeClassic;
        /// <summary>
        /// Toggle which define puzzle mode
        /// </summary>
        public Toggle toggleModePuzzle;
        /// <summary>
        /// Toggle which define custom puzzle mode
        /// </summary>
        public Toggle toggleModeCustomPuzzle;

        /// <summary>
        /// Toggle is allowed win in diffrent destenations
        /// </summary>
        public Toggle toggleWinInAllDestenations;
        /// <summary>
        /// Toggle is allowed using numbers in puzzle
        /// </summary>
        public Toggle toggleUseNumbersPuzzle;
        /// <summary>
        /// Toggle is allowed using numbers in custom image
        /// </summary>
        public Toggle toggleUseNumbersAtCustomImage;


        // Start is called before the first frame update
        void Start()
        {
            CheckUIElements();

            //Load to AddDDList to fix errors
            ddSize.selectedIndex = (int)core.Container.size;
            ddDifficult.selectedIndex = (int)core.Container.difficult;

            SetUIElements();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Checks UI elements, and if error - throws exceptions
        ///<exception cref="CantFindGlobalObj">Generates when SettingsCore is null</exception>
        ///<exception cref="CantFindUIElement">Generates when same UI is null</exception>
        /// </summary>
        void CheckUIElements()
        {
            if (core == null)
                throw new CantFindGlobalObj("Can't find Settings core!");

            if (size == null)
                throw new CantFindUIElement("can't find dropdown of size game field on " +
                    "new game window");
            if (difficult == null)
                throw new CantFindUIElement("Can't find dropdown of difficult on new game window!");

            if (toggleModeClassic == null)
                throw new CantFindUIElement("Can't find toggle of mode, which have " +
                    "variant - classic");
            if (toggleModePuzzle == null)
                throw new CantFindUIElement("Can't find toggle of mode, which have " +
                    "variant - puzzle");
            if (toggleModeCustomPuzzle == null)
                throw new CantFindUIElement("Can't find toggle of mode, which have " +
                    "variant - custom image");

            if (toggleWinInAllDestenations == null)
                throw new CantFindUIElement("Can't find toggle which defines win in all " +
                    "destenations on UI new game window");
            if (toggleUseNumbersPuzzle == null)
                throw new CantFindUIElement("Can't find toggle which defines is need show " +
                    "numbers at puzzle mode");
            if (toggleUseNumbersAtCustomImage == null)
                throw new CantFindUIElement("Can't find toggle which defines is need show " +
                    "numbers at custom image mode");
        }
        
        /// <summary>
        /// Sets UI elements from settrings
        /// </summary>
        public void SetUIElements()
        {
            size.value = (int)core.Container.size;
            difficult.value = (int)core.Container.difficult;

            switch (core.Container.typeGame)
            {
                case ETypeGame.Classic:
                    toggleModeClassic.isOn = true;
                    break;
                case ETypeGame.Puzzle:
                    toggleModePuzzle.isOn = true;
                    break;
                case ETypeGame.PuzzleImage:
                    toggleModeCustomPuzzle.isOn = true;
                    break;
            }

            toggleWinInAllDestenations.isOn =
                core.Container.allowDiffrentDestenationWin;
            toggleUseNumbersPuzzle.isOn =
                core.Container.allowUsingNumbersPuzzle;
            toggleUseNumbersAtCustomImage.isOn =
                core.Container.allowUsingNumbersCustomImage;
        }

        /// <summary>
        /// Method of change property which defines is need win in all destenations
        /// </summary>
        /// <param name="state">State</param>
        public void ChangeWinAllDestenations(bool state)
        {
            core.Container.allowDiffrentDestenationWin = state;
            core.WriteToFile();
        }

        /// <summary>
        /// Method of change property which defines using numbers at puzzle
        /// </summary>
        /// <param name="state"></param>
        public void ChangeUseNumbersPuzzle(bool state)
        {
            core.Container.allowUsingNumbersPuzzle = state;
            core.WriteToFile();
        }

        /// <summary>
        /// Method of change property which defines using numbers at custom image puzzle
        /// </summary>
        /// <param name="state">State</param>
        public void ChangeUseNumbersCustomPuzzle(bool state)
        {
            core.Container.allowUsingNumbersCustomImage = state;
            core.WriteToFile();
        }
    }
}