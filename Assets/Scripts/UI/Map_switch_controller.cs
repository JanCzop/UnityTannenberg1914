using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Map_switch_controller : MonoBehaviour
{
    public Button hex_map_button;
    public Button german_supply_button;
    public Button russian_supply_button;
    public Button control_button;
    public Button german_command_button;

    private Button last_clicked_button;

    private Tester game;

    void Start(){
        game = GetComponentInParent<Tester>();
        hex_map_button.onClick.AddListener(On_hex_map_click);
        //german_supply_button.onClick.AddListener(On_german_supply_click);
        //russian_supply_button.onClick.AddListener(On_russian_supply_click);
        control_button.onClick.AddListener(On_control_map_click);
        german_command_button.onClick.AddListener(On_german_command_button);
    }

    public void On_german_supply_click(){
        //game.hex_painter.Paint_supply_lines
    }

    public void On_russian_supply_click(){
    }

    public void On_german_command_button(){
        game.hex_painter.Paint_command(game.map, game.command_map, Unit.Unit_alliegance.GERMAN);
    }

    public void On_control_map_click(){
        game.hex_painter.Paint_control(game.map,game.control_map);
    }
    public void On_hex_map_click(){
        game.hex_painter.Paint_map_terrain(game.map);
    }

    private void Set_last_clicked(Button button){
        last_clicked_button = button;
    }

    public Button Active_button { get => last_clicked_button; set => last_clicked_button = value; }
    public Tester Game { get => game; set => game = value; }
}
