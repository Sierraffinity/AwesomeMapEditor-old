Nintenlord's Tiled map inserter

**************
* How to use *
**************

-Choose the Tiled map you wish to insert as the Tiled file 
 and choose the ROM you wish to insert to as the ROM.
-Choose the offset where to insert the map and map changes.
-If you wish to, you can choose to insert pointer to map
 and map changes to offsets of your choise. Naturally, 
 you can't insert map change pointer if you don't insert
 map changes.
-Press Run. If insertion succeeds, a box saying "Finished"
 appears. If not, then a box telling what error has
 appeared and the insertion is aborted.
 
**************
* Some notes *
**************

All tiles that will end up in the game should be non-empty.
Empty tiles will cause the insertion to be aborted. Tiles that 
aren't inserted are completely ignored so they can be anything.
Also note that the inserter doesn't support multiple tilesets
on one map.

******************
* Map properties *
******************

You can affect the insertion by adding properties to the map
in Tiled. Poperties supported by this inserter are listed below.
Other properties are ignored. All of the listed parts are Layer 
properties.

-Main:
 Tells the inserter which layer is the map itself.
 Required for maps with more than one layer. Having more
 than one layer marked Main causes an error.
-ID:
 Tells the ID of the map change. Required for map changes.
-X:
 Tells X coordinate of the left edge of the map change. 
 Required for map changes.
-Y::
 Tells Y coordinate of the top edge of the map change. 
 Required for map changes.
-Width:
 Tells the width of the map change. Required for map changes.
-Height:
 Tells the height of the map change. Required for map changes.


*******************
* Version history *
*******************

V 1.0
-First release.
V 1.1
-Added first half of FE6 tilesets.
V 1.2
-Made expand ROM properly when needed.


***********
* Credits *
***********

-Shadowofchaos, for helping with tileset ripping.
-Makers of Tiled, for making such a good program.
-Makers of TiledLib, for code I used in this app (everything in TiledMap folder).
-Nintenlord, for making this program.


****************
* Future plans *
****************

-Tilesets for FE6 and FE8.
-Optionally automate some parts.
-Possibly use Object layers for some basic unit handling.
-More properties to direct insertion.