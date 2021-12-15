# gaston11276 characters NFive Plugin
[![License](https://img.shields.io/github/license/gaston11276/characters.svg)](LICENSE)
[![Build Status](https://img.shields.io/appveyor/ci/gaston11276/characters/master.svg)](https://ci.appveyor.com/project/gaston11276/characters)
[![Release Version](https://img.shields.io/github/release/gaston11276/characters/all.svg)](https://github.com/gaston11276/characters/releases)

NFive Plugin

## Installation
Install the plugin into your server from the [NFive Hub](https://hub.nfive.io/gaston11276/characters): `nfpm install gaston11276/characters`

# playercharacters
Igicore's plugin Characters has been used as a foundation.

F1 - Character creator.
F2 - Appearance menu.

- characters includes character creation supporting code first database as implemented by NFive/IgiCore.
- The migration file assumes that your database already has tables for Users and Sessions as per default.
- Date of Birth is not implemented by ui at this point.
- Most tattoos does not belong to its respective zone but is found in the 'Unknown' zone, temporarly applied at the face tattoo index.
- Props applying/removing is not yet handled properly, thus it might be tricky to have no prop at a certain index (like removing the watch).
- Male/Female selectiono will be improved.
- A button press indication will be added.
- Due to some maximum setting for number of text draw calls made sometime the menues seems to be missing text. This can be seen when opening the 'HeadOverlays' menu. Some solution to this will be found I'm sure.

Please let me know what you think. I am grateful for any suggestion of improvement or just ideas of any kind.
