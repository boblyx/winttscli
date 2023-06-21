# winttscli
Text to speech Command Line Interface for Windows using Microsoft SAPI.

Built with .NET 4.6.1 and compiled using Visual Studio Express 2017. Tested on Windows 11.

## Current Features
- Only Chinese language reading is supported at the moment. Future releases will allow for different languages as long as the relevant speech libraries are installed.
- Convert a single .txt file to be ready by SAPI in the Chinese language to .wav file from command line.
- Read from a folder of .txt files and create a new folder to output .wav files corresponding to the txt files.

## Planned Features
- Be able to specify a language to read in
- Automated conversion from a variety of text formats (HTML, EPUB, PDF etc).
- Automated conversion to a variety of audio formats (MP3, FLAC, OGG etc).
