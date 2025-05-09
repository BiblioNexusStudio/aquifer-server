# ffmpeg

`ffmpeg` and `ffprobe` are used for audio conversion and timing jobs. On development workstations they can be installed via
https://www.ffmpeg.org/download.html for the respective OS and must be added to the path. For deployments, Azure Functions does not allow
installing arbitrary executables. However, because `ffmpeg` can run as a standalone executable, it can be included in the deployment
package and run directly from the file system. The build GitHub Action is responsible for adding `ffmpeg` to the deployment package
in order to avoid the need to commit large binaries to this GitHub repository. The zip file is stored in Blob Storage, downloaded
via GitHub Actions, and unzipped into this folder. After this action, `ffmpeg.exe` (and `ffprobe.exe` if also included) should be
available in this directory (so the full path is `lib/ffmpeg/ffmpeg.exe` and `lib/ffmpeg/ffprobe.exe`).

To update the version of `ffmpeg` used for deployment, download the latest Essentials release from https://www.gyan.dev/ffmpeg/builds/.
Unzip it and copy the license, readme, and necessary executables (from the `bin` folder) into a new local folder named `ffmpeg`.
Zip that folder into a file named `ffmpeg.zip` and drop it into the `deployment-artifacts` blob container in `aquiferstoragedev`.
