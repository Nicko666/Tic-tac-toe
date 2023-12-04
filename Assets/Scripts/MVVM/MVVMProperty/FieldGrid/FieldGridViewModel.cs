using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldGridViewModel : PropertyViewModel<Field>
{
    public ReactiveProperty<PlayerViewModel[][]> tiles = new();
    public PlayerViewModel[][] Tiles => tiles.Value;

    PlayerViewModel[][] _lines;
    public PlayerViewModel[][] Lines => _lines;

    public PlayerViewModel Winner
    {
        get
        {
            PlayerViewModel result = null;

            foreach (var line in _lines)
            {
                if ( line[0].Model != null && line.All(viewModel => viewModel.Model == line[0].Model))
                {
                    foreach (var item in line)
                    {
                        item.InputShow();
                    }
                    return line[0];
                }
            }

            return result;
        }
    }

    public bool Avaliable
    {
        get =>  tiles.Value.Any(line => line.Any(column => column.Model == null));
    }
    
    public Action<PlayerViewModel> onSelected;

    public bool interactible;


    public FieldGridViewModel(PropertyModel<Field> model) : base(model)
    {
        
    }

    public override void OutputProperty(Field value)
    {
        if (tiles.Value != null)
        {
            for (int l = 0; l < tiles.Value.Length; l++)
            {
                for (int c = 0; c < tiles.Value[l].Length; c++)
                {
                    tiles.Value[l][c].onSelected -= InputSelect;
                    tiles.Value[l][c] = null;
                }
                tiles.Value[l] = null;
            }
            tiles.Value = null;
        }


        base.OutputProperty(value);

        if (value != null)
        {
            int size = value.Size;
            PlayerViewModel[][] playerViewModels = new PlayerViewModel[size][];

            for (int l = 0; l < size; l++)
            {
                playerViewModels[l] = new PlayerViewModel[size];
                for (int c = 0; c < playerViewModels[l].Length; c++)
                {
                    playerViewModels[l][c] = new PlayerViewModel(null);
                    playerViewModels[l][c].onSelected += InputSelect;
                }
            }
            //Debug.Log(playerViewModels.Length + " / " + playerViewModels[0].Length);
            tiles.Value = playerViewModels;
        }

        _lines = GetLines().ToArray();

        IEnumerable<PlayerViewModel[]> GetLines()
        {
            int size = _model.property.Value.Size;

            //horizontal lines

            for (int line = 0; line < size; line++)
            {
                yield return tiles.Value[line];
            }

            //vertical lines

            for (int column = 0; column < size; column++)
            {
                var newColumn = new PlayerViewModel[size];

                for (int line = 0; line < size; line++)
                {
                    newColumn[line] = tiles.Value[line][column];
                }

                yield return newColumn;
            }

            //diagonal lines

            var diagonalLineRight = new PlayerViewModel[size];

            for (int i = 0; i < size; i++)
            {
                diagonalLineRight[i] = tiles.Value[i][i];
            }

            yield return diagonalLineRight;

            var diagonalLineLeft = new PlayerViewModel[size];

            for (int i = 0; i < size; ++i)
            {
                diagonalLineLeft[i] = tiles.Value[i][(size - 1) - i];
            }

            yield return diagonalLineLeft;

        }
    
    }

    public void InputSelect(PlayerViewModel playerViewModel)
    {
        if (interactible)
            onSelected?.Invoke(playerViewModel);
    }

}