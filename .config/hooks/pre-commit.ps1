#!/usr/bin/env pwsh

# Get the list of staged files, including status
$stagedChanges = git diff --name-status --cached

# Stage all changes, perform formatting, then unstage
git add . > $null
dotnet format style
dotnet format analyzers
dotnet csharpier .
git reset > $null

# Re-stage only the originally staged files based on their status
$stagedChanges -split "`n" | ForEach-Object {
    $status, $file = $_ -split "`t", 2
    switch ($status.Substring(0, 1)) {
        'A' { git add $file > $null }
        'M' { git add $file > $null }
        'D' { git rm $file > $null }
        'R' {
            $oldFile, $newFile = $file -split "`t"
            git add $oldFile > $null
            git add $newFile > $null
        }
    }
}
