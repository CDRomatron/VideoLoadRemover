# VideoLoadRemover
A video based load remover based on the CTTR Load Remover. This application bases on a video feed of the gameplay, so is intended for runs performed on a console with a capture card.

## Before we get started...
Please note, that due to the nature of video feed based applications, you will never get frame accurate results. This tool is intended for use for games with loading screens with durations that can significantly vary to give runners a more accurate idea of pace. This application should not be treated as a replacement for manual load timing.

## How to use
Download the latest release from https://github.com/CDRomatron/VideoLoadRemover/releases

### Configuring
First, place the VideoLoadRemover folder in your livesplit directory. Run the VideoLoadRemover.exe and click start. Increase the X, Y, width, and height so that the game feed shows whatever clear indicator your game has that it is loading.

For example the game Castlevania: Curse of Darkness has this symbol in the corner whilst loading.

![Example Preview](https://i.imgur.com/EQjNc7N.png)

Once you have aligned the capture cause the game to enter a loading screen then click "save load-screen".

Now you will notice a number rapidly changing in the bottom right corner, this number is how certain the program is that the screen is loading. The closer the number is to zero, the more certain the program is.

Note how low the values go in the correct circumstances, then click stop. In the threshold box, enter a number which is more than the highest number the program reached in the correct circumstances. 

To test the software, click start, and watch the Is Loading checkbox in the bottom right corner. If the program believes the game is loading, the box will be ticked, and when it is not loading, it will be unticked.

When you are happy with your configuration, click "Save Settings". You can also load your settings at a later date.

### Running
Now, start Livesplit, go to Edit Layout, and add a "Scriptable Autosplitter". Select VideoLoadRemover.asl. As long as you have clicked Save Settings in the above step, this should now pause the time during load screens. Please remember to swap your comparision to game time, as it does not affect real time.

If you want to test your settings, you can also use the VideoLoadTimer.asl file, which is always paused except on loading screens.

## Building from source
Clone the solution, and build. This application does not currently have any dependancies.

## Contact
If you are having issues with this application, have questions, or want to suggest improvements, please contact me on [Twitter @CDRomatron](https://twitter.com/CDRomatron), on Discord @CDRomatron (Mike)#6527, on [Speedrun.com @CDRomatron](https://www.speedrun.com/CDRomatron), or leave an issue on the project issue tracker. Also, if you like this tool and want to support the developement of this and other projects you can do so here [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/X8X4247YU)
