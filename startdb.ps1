Write-Host ""
$rebuildrequired = Read-Host -Prompt "Is a code rebuild required? (y/n)"
if (($rebuildrequired -eq "y") -Or ($null -eq $rebuildrequired)){
    docker-compose up --build -d
}
Else {
    docker-compose up -d
}
Write-Host ""

Write-Host "Backend Databases now up and running"
Read-Host -Prompt "Press Enter to shut stack down exit"
docker-compose down