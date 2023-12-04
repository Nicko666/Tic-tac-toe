using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "NewPlayerBehaviour", menuName = "ScriptableObjects/PlayerBehaviour")]
public class PlayerBehaviour : ScriptableObject, ISprite
{
    [SerializeField] Sprite _sprite;

    //[SerializeField] PlayerBehaviourType _types;

    public Sprite GetSprite() => _sprite;

    [Header("BehaviourSettings")]
    [SerializeField] bool _playerInput;
    [SerializeField] bool _useSelfOwnedLines;
    [SerializeField] bool _blockNextPlayerWin;
    [SerializeField] bool _useSelfFilledLines;
    [SerializeField] float _thinkingTime;

    PlayerViewModel selectedViewModel;


    public async void Action(FieldGridViewModel fieldGridViewModel, PlayersQueueViewModel playersQueueViewModel)
    {
        //if player
        if (_playerInput)
        {
            fieldGridViewModel.interactible = true;
            return;
        }


        var taskWait = Wait(_thinkingTime);
        var taskSelectedCell = Select(fieldGridViewModel, playersQueueViewModel);
        
        await Task.WhenAll(taskWait, taskSelectedCell);

        fieldGridViewModel.onSelected(selectedViewModel);

    }

    public async Task Wait(float seconds)
    {
        await Task.Delay((int)(seconds * 1000));
    }

    public async Task Select(FieldGridViewModel fieldGridViewModel, PlayersQueueViewModel playersQueueViewModel)
    {
        //all avaliable lines
        Dictionary<PlayerViewModel, int> cellsRaiting = new();

        foreach (var line in fieldGridViewModel.Lines)
        {
            if (!line.Any(item => item.Model == null))
                continue;

            //lineData
            Dictionary<PlayerModel, int> lineContent = new();
            foreach (var cell in line)
                if (cell.Model != null)
                {
                    if (lineContent.ContainsKey(cell.Model))
                        lineContent[cell.Model] += 1;
                    else
                        lineContent.Add(cell.Model, 1);
                }

            foreach (var cell in line)
            {
                //if this empty
                if (cell.Model != null)
                    continue;

                //add to list
                if (!cellsRaiting.ContainsKey(cell))
                    cellsRaiting.Add(cell, 0);

                //if all empty
                if (lineContent.Count == 0)
                    cellsRaiting[cell] += 1;

                //if line but this is ocupated by same
                if (lineContent.Count == 1)
                {
                    // if line has owned
                    if (_useSelfOwnedLines)
                        if (lineContent.ElementAt(0).Key == playersQueueViewModel.FirstPlayer.Model)
                            cellsRaiting[cell] += 1;

                    if (lineContent.ElementAt(0).Value == line.Count() - 1)
                    {
                        cellsRaiting[cell] += 1;

                        // if all exept one owned
                        if (_blockNextPlayerWin)
                            if (lineContent.ElementAt(0).Key == playersQueueViewModel.SecondPlayer.Model)
                                cellsRaiting[cell] += 1;

                        // if all exept one owned by self
                        if (_useSelfFilledLines)
                            if (lineContent.ElementAt(0).Key == playersQueueViewModel.FirstPlayer.Model)
                                cellsRaiting[cell] += 1;
                    }
                }
            }

        }

        if (cellsRaiting.Count == 0) throw new System.InvalidOperationException("no empty cell");

        //int maxValue = cellsRaiting.Max().Value;
        int maxValue = 0;
        foreach (var cell in cellsRaiting)
            if (cell.Value > maxValue)
                maxValue = cell.Value;

        List<PlayerViewModel> maxCells = new();
        foreach (var cell in cellsRaiting)
            if (cell.Value == maxValue)
                maxCells.Add(cell.Key);

        selectedViewModel = maxCells[Random.Range(0, maxCells.Count)];
    
    }


}



//public enum PlayerBehaviourType
//{
//    Input,
//    Random,
//    Protective,
//    Aggressive
//}