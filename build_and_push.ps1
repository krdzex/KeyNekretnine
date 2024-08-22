function Write-Color {
    param (
        [string]$Text,
        [string]$Color = "White"
    )
    Write-Host $Text -ForegroundColor $Color
}

function Check-Docker {
    try {
        docker info > $null 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-Color "Docker is not running. Opening Docker Desktop..." "Yellow"
            $dockerDesktopPath = "C:\Program Files\Docker\Docker\Docker Desktop.exe"
            
            if (Test-Path $dockerDesktopPath) {
                Start-Process $dockerDesktopPath
                Write-Color "Waiting for Docker to initialize..." "Cyan"
                $timeout = 60
                $elapsed = 0
                $interval = 2
                while ($elapsed -lt $timeout) {
                    Write-Color "Waiting for Docker to initialize... (Elapsed: $elapsed seconds)" "Cyan"
                    Start-Sleep -Seconds $interval
                    docker info > $null 2>&1
                    if ($LASTEXITCODE -eq 0) {
                        Write-Color "Docker has been started successfully!" "Green"
                        return
                    }
                    $elapsed += $interval
                }
                Write-Color "Failed to start Docker. Please start Docker manually." "Red"
                exit 1
            } else {
                Write-Color "Docker Desktop application not found at $dockerDesktopPath" "Red"
                exit 1
            }
        } else {
            Write-Color "Docker is already running." "Green"
        }
    } catch {
        Write-Color "Error checking Docker status: $_" "Red"
        exit 1
    }
}

Write-Color "============================" "Cyan"
Write-Color "   Starting Build Process    " "Magenta"
Write-Color "============================" "Cyan"

Check-Docker

Write-Color "`nNavigating to the src directory..." "Cyan"
cd src
Write-Color "Current Directory: $PWD" "Gray"

Write-Color "`nBuilding the Docker image..." "Cyan"
docker build --platform="linux/amd64" . -t krdzex/keynekretnineapi:latest

if ($LASTEXITCODE -eq 0) {
    Write-Color "Docker image built successfully!" "Green"
} else {
    Write-Color "Docker image build failed!" "Red"
    cd ..
    exit $LASTEXITCODE
}

Write-Color "`nPushing the Docker image to the registry..." "Cyan"
docker push krdzex/keynekretnineapi:latest

if ($LASTEXITCODE -eq 0) {
    Write-Color "Docker image pushed successfully!" "Green"
} else {
    Write-Color "Docker image push failed!" "Red"
    cd ..
    exit $LASTEXITCODE
}

Write-Color "`nNavigating to original directory ..." "Cyan"
cd ..
Write-Color "Current Directory: $PWD" "Gray"

Write-Color "`n============================" "Cyan"
Write-Color "   Build And Push Process Complete    " "Magenta"
Write-Color "============================" "Cyan"
