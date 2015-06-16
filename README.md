# SC-Virus-Life-Simulation
This is a Virus Life Simulation. 
The application was created with Unity 3D and C#. 
Now the target is Android but Unity allow to publish in almost all formats.

![ScreenShot](http://www.toohook.it/public/1.jpg)

# Logic
The logic of a virus is very simple. 
When the virus touch the cell the virus inject its genetic code inside it, after a period of incubation the cell die and a new virus come out.
All (or almost) the parameters that drive the AI are settable throw class named Settings.

# Behavior
The virus and the cells move around the screen.
The movement of cells is very slow instead the virus are more quickly.

When virus touch cell the virus remains attached to it for a while and try to inject a infection.
The number of passed infections change from zero to a number specified in the Settings.

After a cell is infected you will see the infection grow into the cell for a incubation period.
The cell die and the new virus come out and start to go around the screen.

# Enviroment
The enviroment influence the life of ecosystem.

- TEMPERATURE -> influence the cells/virus movement velocity and the life duration
- pH -> influence the incubation period and the cells/virus life duration

NB: under zero degree the ecosystem is stopped but cells and virus not die because they are ibernate :-)

# Victory
NO HAVE! But the game finish when all virus are died.

# Istallation
In the main directory you have a SCV.apk for install the app on a Android smartphone.
If you have Unity 3D (free version available on site) clone the project and run Unity.

Tested on Samsung Galaxy S4 mini


THANKS
