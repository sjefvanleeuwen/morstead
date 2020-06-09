echo "install rancher os container with portainer"
docker-machine create -d virtualbox^
        --virtualbox-boot2docker-url https://releases.rancher.com/os/latest/rancheros.iso^
        --virtualbox-memory 4096^
        --provider hyperv^
    orleans-dev-server

echo "deploy portainer"
docker-machine ssh orleans-dev-server docker volume create portainer_data
docker-machine ssh orleans-dev-server docker run -d -p 9000:9000 --name portainer --restart always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer

echo "deploy orleans as saas for development purposes"
curl https://raw.githubusercontent.com/sjefvanleeuwen/fieldlab-reference-architecture/master/compositions/5Layer/docker-compose.yml --output docker-compose.yml
docker-machine scp -r docker-compose.yml orleans-dev-server:/home/docker/docker-compose.yml
docker-machine ssh orleans-dev-server docker run --name docker-compose -v /home/docker/docker-compose.yml:/docker-compose.yml -v /tmp:/tmp -v /var/run/docker.sock:/var/run/docker.sock -w /tmp docker/compose:1.24.0 up -d

docker-machine ssh orleans-dev-server export PATH=$PWD/bin:$PATH