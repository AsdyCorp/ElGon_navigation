
# ElGon_navigation

**The author is not responsible for any damages, any tests on your responsibility.**

Project on the study of human navigation in space by the method of electronic stimulation of skin zones based on data from the TOF camera. The project is aimed at supplementing the sensorium of a healthy person and helping the blind and visually impaired.

This work is licensed under a [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.](http://creativecommons.org/licenses/by-nc-sa/4.0/)


## Concept

The project is based on the use of a camera with a TOF sensor (siped a010 for example). The image of the depth map is transmitted to a microcontroller, where the depth map is compressed and converted into an array of signals for transmission to sensitive areas of the skin. Using the dac on the microcontroller, we can change the voltage of signal, signaling the distance to the obstacle.

![TOF camera image](https://raw.githubusercontent.com/AsdyCorp/ElGon_navigation/main/Images/4.png)
tof camera image example

![conceptual scheme](https://raw.githubusercontent.com/AsdyCorp/ElGon_navigation/main/Images/5.jpg)
conceptual scheme
## Tests

Without a sensor and a suitable controller, it was decided to simulate the operation of the controller and sensor inside the Unity3d game engine. A virtual obstacle course was built. From the camera of the game character, rays are sent according to each pixel of the real sensor (for 100 * 100, this is 10,000 rays with a frequency of 20 (real sensor)-50 (virtual sensor) rays per second). Next, using downsampling, the array of distances to objects is converted into a 2*2 array, which is transmitted to the Arduino via a serial interface, where it is referred to the area of the skin near the tip of the nose through the a0-a3 analog pins. Ground pin was connected to tongue. During the tests, I managed to pass the obstacle course with my eyes closed, but the lack of voltage control through the dac to display the distance to the object and the low resolution of the electrode array make navigation difficult.

![Unity3d simulation of sensors](https://raw.githubusercontent.com/AsdyCorp/ElGon_navigation/main/Images/1.png)
Unity3d simulation of sensors


![prototype of device using in tests](https://raw.githubusercontent.com/AsdyCorp/ElGon_navigation/main/Images/2.jpg)
prototype of device using in tests

![pins connected to skin](https://raw.githubusercontent.com/AsdyCorp/ElGon_navigation/main/Images/3.jpg)
pins connected to skin

The project will be continued in the future. Larger electrode arrays and a combination of other stimuli (heat, sound, vibration) on the skin will be tested for better signaling of information about the surrounding world.
