class AAAAAAAAAA
{/*
  


     TODO FIRST
-------------------------------------------------

    Finish/fix DisplayInitializer and ShipShop
        need to have a way to bring up shop menu. end of level?
        not sure about instantiating/initializing the buttons. seems hard, might just do disable button instead

    Fix magnet situation/itemSO stuff. 
        not updating HUD.

    Player immortal after changing ships

    Enemies stop shooting eventually. might be related to changing ships

    Finish/fix powerupSO stuff.
  
    Make screen auto fit to whatever resolution, then build player bounds, spawn points, etc based off that.

    Fix bullet sorting layers with enemies/player. maybe just put bullets under everything
        or could change the sorting layer from each gun? kinda dumb if its always one sprite per ship

    Make sounds/music



    TODO
-------------------------------------------------------------------------------------

    Put a timer on each level, and when that timer is up, have a boss fight. 
        Boss stays on screen instead of just flying past like normal enemies.
        When boss dies, level over.
            Go to level end screen.
            Shows level stats. Kills, money collected, etc..
            Story cutscene stuff here later maybe.
            Shop/Go to next level menu.
            Have random mini games fit in with the plot.
                Mini-platformer, Puzzles, Defend the base (Tower defense), whatever 

    Maybe get rid of invulnerability frames, bring back knockback? or have neither?
        make knockback look better
        compare player and enemy knockbackForce on collision. loser gets knockbacked by difference?
        use velocities too? inertia/ real physics?

    Make sprites
        make enemy sprites
        maybe some bosses
        gun sprites
        make base sprites so that wings, noses, guns, etc all line up. then draw the actual sprites around those

    4+11+16+18+21+8+5+1=84

    
    FIXED???
-----------------------------------------------------------------------------------

    Make each gun fire at its individual rate while fire held down
    
    Fix becoming invulnerable after dying once
    

    FIXED
----------------------------------------------------------------------

    Fix playerMovement, able to go off screen as is
        "Game" screen in unity had scale set to like 1.5x

    Objects being disabled on screen load without me telling them to 
        unsubscribe (in onDisable) from every event you subscribe to (in onEnable)

    Fix playerShoot y-offset problem
        the waitforseconds made the ship move before the guns instantiated

    Property not working
        accidentally capitalized the variable name in the return part of the getter




---------------------------
    GAME OVERVIEW/IDEAS
---------------------------

    Space shooter

    Can have different ships with different stats and number of guns, etc

    Can have different guns

    Pilot has stats that add bonuses to any ship flown

    Maybe have a crew eventually. members add bonuses or something
    







     
*/}