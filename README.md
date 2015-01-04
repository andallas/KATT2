###KATT 2

Sequel to KATT.


####Objects

Asteroid:
```
An object that is easily destroyed, but turns into multiple smaller asteroids that fly out away from the center, optionally with the bullet's vector added.
```

Wall:
```
An essentially indestructible object, generally square.
```

####Enemies

#####Weak

Popcorn:
```
A small, easily destroyed enemy that provides little, if any threat. Primarily used for points, distracting the player, or allowing a player to continue a combo chain.
```

PopcornShoot:
```
Very similar to the Popcorn, but this variant has the ability to shoot, though it has a slow firerate.
```

Saucer:
```
A small, easily destroyed enemy that provides no threat.  It moves across the top of the screen, or in a generally difficult to hit position.  It is worth an inordinately large amount.
```

Cargo Pod:
```
An enemy type that, when destroyed, drops a power-up.  In some games, these are completely passive, while others will have an active offense.
```


#####Basic

Turret:
```
An immobile enemy type that attacks by firing directly towards the player ship.
```

Sine Ship:
```
A small ship that uses the sine function to generate a movement behavior that is somewhat tricky to hit.  Particularly effective in Squadrons.  Generally attack by colliding with the player.
```

Hunter-Seeker:
```
An enemy that attacks by flying directly at the player character.
```

Tank:
```
A slow-moving ground enemy.  Takes two normal shots to destroy.  The first shot destroys the turret mounted to its top.  The second shot destroys the body.
```

Snake:
```
This enemy consists of a chain of enemies all following a "head" enemy at a set distance.
```

Twin Gunner:
```
The wide ship that shoots two streams of bullets from widely separated left and right cannons.
```

Dive Bomber:
```
An enemy that approaches the bottom or side of the screen in a relatively slow, easy to target path.  When it reaches a critical distance, it releases its bullets and then swoops back to the other end of the screen rather quickly.
```

Wing:
```
A small group of ships acting together.
```

Flood:
```
Very large group of enemies unable to shoot, simply trying to ram you. Most commonly found in arena shmups, but such attacks sometimes appear in normal shooters.
```


#####Tough

Gunner:
```
A weak enemy that compensates by firing lots of bullets in a single direction.  Firing generally occurs in bursts, so a skilled player can dodge between bursts.
```

Spammer:
```
A big fat enemy that takes slightly above average damage to destroy that fires out semi-random bursts of bullets (like a shotgun).
```

Sprayer:
```
Similar to the gunner, but instead of shooting frequently, shoots a spray of two or more shots outward.
```

Multi Gunner:
```
The long ship that open to uncover an array (usually two columns in vertical shooters) of identical cannons, missile launchers, etc. that fire in synchronized volleys; usually large but easily disarmed.
```

Supersonic:
```
The fast and resilient enemy, usually a fighter jet, that has a particularly high probability of escaping alive, in many cases retreating after a brief exposure; not necessarily shooting, since it is suitable as a distraction, a scoring difficulty or a Seeker.
```

Spawner:
```
This enemy produces smaller "child" enemies.
```

Suicide Bomber:
```
This enemy paths towards a given position, and then explodes in a burst of bullets.
```

Zits:
```
This enemy is similar to a suicide bomber, but will only explode in a burst of bullets when the player shoots it.
```

Scrolling Thunder:
```
The train or ship that moves forward ahead of the player rather than scrolling away, greatly extending its exposure to player fire. This is a common pattern for bosses and minibosses, and it's usually contrasted with regular scrolling enemies.
```

Sniper:
```
Appears from lower corner or bottom of the screen and shoots you from behind (Raiden is particularly full of those enemies)
```


####Bosses

Sub-Boss/Mini-Boss:
```
A larger than average enemy with more life and more threatening attacks than the average enemy in a given level.  The level does not end when they are defeated.  Sometimes, if they are not defeated by the time a given timer counts down to zero, they will flee.
```


######Types

Platform:
```
A larger ship that has no attack of its own, but is covered with multiple turrets.  It has a single weak spot that can be damaged.  When its health reaches zero, it explodes, destroying any turrets left on top for no points.
```


Boss:
```
A much larger than average enemy that should be the most threatening and resilient enemy on a given level.  Often, a boss will return  in a later level as a sub-boss.
```

######Types

Flyweight:
```
The small boss with high mobility and high firepower, typically similar to the player's ship or humanoid. Example: most of the Touhou games, Sinistar.
```

Middleweight:
```
The medium boss (shown in its entirety) that fills a large part of the screen but can move significantly. Examples: Darius series; the large aircraft in most Raiden III levels (which move just 
enough to force the player to track them).
```

Cyclops:
```
The boss that stays in the same place; usually small or medium (leaving a large part of its side of the screen unused or available for its flunkies and shots) and sometimes large enough to fill the whole screen. There is a typical tactical theme: staying in front of the boss is more dangerous, but it's the only way to damage it.
```

Multiple Arena Boss:
```
The gigantic boss that comes into the crosshairs following a preset schedule (sometimes oblivious, sometimes stopping until the player destroys the accessible turrets and sections); tends to be a battleship or a train.
```

Mutator:
```
Typically Japanese: the boss that instead of dying changes to a different form. Some forms might be weak and short-lived, only meant as a logical transition. Animation and/or dialogue intermissions might introduce the new form's appearance. Many examples, but overdone in the recent "The Trapezohedron of the darkness".Classic subtypes include people (in a loose sense...) who change dresses and switch to nastier firing patterns, destructible vehicles that become angrier and more mobile as the player blows away their pieces, combining mecha that join with large accessories as they suffer, and regular looking ships that crack open to reveal weird things (e.g. Raiden).
```

####Other

Pinwheel:
```
More of an attack type than a specific ship type (may need to break these out into movement types and attack types).  Pinwheel attack shoots bullets out at regular intervals from a central point.  The angle of release changes over time, creating a curved pattern of bullets.
```

Traffic:
```
Non-combatant moving targets (citation: DoDonPachi level 4 start).  Can contact them without any damage.  Bullets will destroy them.
```