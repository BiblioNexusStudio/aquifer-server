# ffmpeg

`ffmpeg` and `ffprobe` are used for audio conversion and timing jobs. On development workstations they can be installed via
https://www.ffmpeg.org/download.html for the respective OS and must be added to the path. For deployments, Azure Functions does not allow
installing arbitrary executables.  However, because `ffmpeg` can run as a standalone executable, it can be included in the deployment
package and run directly from the file system. The build GitHub Action is responsible for adding `ffmpeg` to the deployment package
in order to avoid the need to commit large binaries to this GitHub repository. The zip file is stored in Blob Storage, downloaded
via GitHub Actions, and unzipped into this folder.  After this action, `ffmpeg.exe` and `ffprobe.exe` should be available in this directory
within a subdirectory named `bin` (so the full path is `lib/ffmpeg/bin/ffmpeg.exe` and `lib/ffmpeg/bin/ffprobe.exe`).
