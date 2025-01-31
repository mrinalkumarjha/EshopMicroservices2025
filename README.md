# EshopMicroservices2025

#docker

Add yaml file using add orchrestator support from project menu.

we add service indo in docker.compose file. and environment variable in override file.

Once we are done with adding image in docker compose and override just run docker Compose profile from visual studio. this will
automatically pull image and congigure image as per our configuration. it will internally run docker compose up command

# docker commnnd 

run this command from source dir where yml file is available.
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build


