using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clash : MonoBehaviour
{
    public int MIN_DICE_RESULT = 1;
    public int MAX_DICE_RESULT = 6;

    private static System.Random randomizer = new();
    private (List<Hex> german, List<Hex> russian) hexes;
    private ((List<Unit> troops, Unit commander) german, (List<Unit> troops, Unit commander) russian) units;
    private (Order_card german, Order_card russian) orders;
    private (Allegiance attacker, Allegiance defender) role;
    private Battle_preview preview;
    private Allegiance initiative_faction;

    private Control_map map_control;


    public enum Order_card{ATTACK,DEFEND,ASSAULT,RETREAT}
    public enum Battle_preview{ATTACK_VS_DEF,ATTACK_VS_RET,BOTH_ATTACK,NO_FIGHT}


    private (int,int) Calculate_units_powers(){
        (int german, int russian) power;
        power.german = 0;
        power.russian = 0;

        foreach(Unit german_unit in units.german.troops) power.german += Calculate_single_unit_power(german_unit, Allegiance.GERMAN);
        foreach(Unit russian_unit in units.russian.troops) power.russian += Calculate_single_unit_power(russian_unit, Allegiance.RUSSIAN);

        return power;
    }

    private int Calculate_single_unit_power(Unit unit, Allegiance allegiance){
        int power = unit.Get_current_firepower();
        if(unit.Type == Unit.Unit_type.CAVALRY && !Is_this_only_cavalry_fight())
            power = Divide_by_2_and_round_up(power);
        else if(unit.Type == Unit.Unit_type.ARTILLERY && this.role.defender == allegiance)
            power = Divide_by_2_and_round_up(power);
        return power;
    }
    private int Divide_by_2_and_round_up(int x){
        if(x % 2 == 1) return x/2+1;
        else return x/2;
    }


    private void Set_how_fight_takes_place(){
        if ((orders.german == Order_card.DEFEND || orders.german == Order_card.RETREAT) &&
            orders.russian == Order_card.DEFEND || orders.russian == Order_card.RETREAT) this.preview = Battle_preview.NO_FIGHT;
        
        else if ((orders.german == Order_card.ATTACK || orders.german == Order_card.ASSAULT) &&
                orders.russian == Order_card.DEFEND) this.preview = Battle_preview.ATTACK_VS_DEF;

        else if ((orders.russian == Order_card.ATTACK || orders.russian == Order_card.ASSAULT) &&
                orders.german == Order_card.DEFEND) this.preview = Battle_preview.ATTACK_VS_DEF;

        else if ((orders.german == Order_card.ATTACK || orders.german == Order_card.ASSAULT) &&
                (orders.russian == Order_card.ATTACK || orders.russian == Order_card.ASSAULT))
                this.preview = Battle_preview.BOTH_ATTACK;

        else if ((orders.german == Order_card.RETREAT && orders.russian != Order_card.DEFEND) ||
                (orders.russian == Order_card.RETREAT && orders.german != Order_card.DEFEND))
                this.preview = Battle_preview.ATTACK_VS_RET;

        else this.preview = Battle_preview.NO_FIGHT;
    }

    private void Set_who_is_attacking(){
        if(preview == Battle_preview.ATTACK_VS_DEF){
            if(orders.german == Order_card.ATTACK || orders.german == Order_card.ASSAULT) {
                role.attacker = Allegiance.GERMAN;
                role.defender = Allegiance.RUSSIAN;
            }
            else {
                role.attacker = Allegiance.RUSSIAN;
                role.defender = Allegiance.GERMAN;
            }
        }
        else if(preview == Battle_preview.ATTACK_VS_RET){
            if(orders.german == Order_card.ATTACK || orders.german == Order_card.ASSAULT)
                role.attacker = Allegiance.GERMAN;
            else role.attacker = Allegiance.RUSSIAN;
        }
        else if(preview == Battle_preview.BOTH_ATTACK){
        
        }
    }
    

    private void Determine_who_is_attacking_int_open_battle(){
        bool is_attacker_determined = false;
        int german_result, russian_result;
        while(!is_attacker_determined){

            german_result = randomizer.Next(MIN_DICE_RESULT,MAX_DICE_RESULT+1) + Seek_most_effective_unit(units.german.troops);
            russian_result = randomizer.Next(MIN_DICE_RESULT,MAX_DICE_RESULT+1) + Seek_most_effective_unit(units.russian.troops);

            if(initiative_faction == Allegiance.GERMAN) german_result++;
            else russian_result++;

            if(german_result == russian_result){
                german_result = 0;
                russian_result = 0;
            }
            else{
                if(german_result > russian_result){
                    role.attacker = Allegiance.GERMAN;
                    role.defender = Allegiance.RUSSIAN;
                }
                else {
                    role.attacker = Allegiance.GERMAN;
                    role.defender = Allegiance.RUSSIAN;
                }
                is_attacker_determined = true;
            }
        }
    }

    private int Seek_most_effective_unit(List<Unit> units){
        int result = 0;
        foreach(Unit unit in units){
            if(unit.Get_current_efficiency() > result) result = unit.Get_current_efficiency();
        }
        return result;
    }

    private List<Hex> Which_area_is_smaller(){
        if(hexes.german.Count == hexes.russian.Count) return null;
        else return hexes.german.Count > hexes.russian.Count ? hexes.german : hexes.russian;
    }
    private bool Is_defending_force_surrounded(Hex hex){
        Allegiance defender_allegiance = hex.Allegiance;
        List<int> indexes = hex.Get_edges_indexes();
        foreach(int index in indexes) if(indexes.Contains(Hex.Get_opposite_edge_index(index))) return true;
        // TODO: CONSIDER IF ITS WORTH TO HAVE TRIANGLE SURROUNDING
        return false;
        
    }
    private bool Is_this_only_cavalry_fight(){
        List<Unit> merged_list = new(); 
        merged_list.AddRange(units.german.troops);
        merged_list.AddRange(units.russian.troops);

        foreach(Unit unit in merged_list) 
            if (unit.Type == Unit.Unit_type.INFANTRY || unit.Type == Unit.Unit_type.ARTILLERY) return false;
        return true;
    }



    public Control_map Map_control { get => map_control; set => map_control = value; }
    public ((List<Unit> german, Unit german_commander), (List<Unit> russian, Unit russian_commander)) Units { get => units; set => units = value; }
    public (Allegiance attacker, Allegiance defender) Role { get => role; set => role = value; }
    public (Order_card german, Order_card russian) Orders { get => orders; set => orders = value; }
    public Battle_preview Preview { get => preview; set => preview = value; }
    public Allegiance Initiative_faction { get => initiative_faction; set => initiative_faction = value; }
    public static System.Random Randomizer { get => randomizer; set => randomizer = value; }
}
