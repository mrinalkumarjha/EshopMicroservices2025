# EshopMicroservices2025

#docker

Add yaml file using add orchrestator support from project menu.

we add service indo in docker.compose file. and environment variable in override file.

Once we are done with adding image in docker compose and override just run docker Compose profile from visual studio. this will
automatically pull image and congigure image as per our configuration. it will internally run docker compose up command

# docker commnnd 

docker compose  -f "C:\Mrinal\Projects\EshopMicroservices2025\src\docker-compose.yml" -f "C:\Mrinal\Projects\EshopMicroservices2025\src\docker-compose.override.yml" -p dockercompose12704217255425201877 --ansi never up -d --build --remove-orphans
