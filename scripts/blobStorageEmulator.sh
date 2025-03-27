# check if azurite is installed
if ! [ -x "$(command -v azurite)" ]; then
# install azurite
npm install -g azurite
fi

azurite --silent --location c:\azurite --debug c:\azurite\debug.log