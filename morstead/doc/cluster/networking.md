# MySQL Cluster Installeren

## Wat is MySQL-cluster?

MySQL Cluster is gebouwd op de NDB-opslagengine en biedt een zeer schaalbare, realtime, ACID-compatibele transactiedatabase, die 99,999% beschikbaarheid combineert met de lage TCO van open source. Ontworpen rond een gedistribueerde, multi-master architectuur zonder single point of failure, schaalt MySQL Cluster horizontaal op standaard hardware om lees- en schrijfintensieve workloads te bedienen, toegankelijk via SQL- en NoSQL-interfaces.

`Morestead` gebruikt de MySQL Database als opslag voor Orleans via een beschikbare `ADO.NET` Provider. De MySQL provider biedt via `ADO.NET` de volgende connectoren voor MySQL:

* Storage voor het Orleans Cluster (membership)
* Persistence voor Grain States
* Persistence voor Reminders

Voor hoge uptime garantie is het noodzakelijk dat dit cluster geinstalleerd wordt.

## Installeer 3 docker machines.

Installeer 3 Linux machines met een boot2docker.iso. Dit kan op een VM of op Bare Metal.
De 3 machines worden daarna als volgt ingericht:

1. MySQL Master Node (inclusief mysql deamon en cluster management tools)
2. MySQL Data Node 1
3. MySQL Data Node 2

## Netwerkinstellingen bewaren

Meestal worden bij het uitvoeren van de Tiny Core Linux disto de instellingen opgeslagen in `/opts`. Echter bij het opstarten binnen de boot2docker aangepaste disto worden deze instellingen gereset bij het opnieuw opstarten van de machine. De instellingen moeten worden opgeslagen als onderdeel van het `boot2docker profiel`.

### Boot2Docker Profiel

Het boot2docker-profiel bevindt zich op `/var/lib/boot2docker /profile`. We zullen een script schrijven en opslaan
nodig om de netwerkinstellingen blijvend te veranderen.

#### SSH naar de MySql master node machine

```
ssh -l docker 192.168.178.74
```

The authenticity of host '192.168.178.74 (192.168.178.74)' can't be established.
ECDSA key fingerprint is SHA256:9xQAF97+OkzdTnXjH3NXUt+YvFak57Jjo53qGpaPkCE.
Are you sure you want to continue connecting (yes/no)? yes
Warning: Permanently added '192.168.178.74' (ECDSA) to the list of known hosts.
docker@192.168.178.74's password:

```
   ( '>')
  /) TC (\   Core is distributed with ABSOLUTELY NO WARRANTY.
 (/-_--_-\)           www.tinycorelinux.net
```

#### Editing boot2docker Profile

Om het permanente profiel te bewerken, moeten we een editor installeren. Tiny Core Linux
wordt geleverd met TCE (Tiny Core Extensions). Deze bibliotheek bevat de `nano` editor. Deze extensie moet eerst toegevoegd worden zodat vervolgens het profiel bewerkt kan worden:

```
docker@boot2docker:~$ sudo tce -wi nano
docker@boot2docker:~$ sudo nano /var/lib/boot2docker/profile
```

#### Script for Profile 

Het netwerkprofielscript is als volgt, het systeem in dit voorbeeld is opgezet met een secundaire
Ethernet-interface, dus in plaats van eth0 gebruiken we de eth1 interface. Maar natuurlijk kan een opstelling
variëren.

```
#========================================================
#!/bin/sh
# kill dhcp client for eth1
if [ -f /var/run/udhcpc.eth1.pid ]; then
kill `cat /var/run/udhcpc.eth1.pid`
sleep 0.1
fi
# configure interface eth1
sudo ifconfig eth1 192.168.0.2 netmask 255.255.255.0 broadcast 192.168.0.255 up
sudo ip addr add 192.168.0.10 dev eth1
sudo route add default gw 192.168.0.254
sudo echo nameserver 192.168.0.254 >> /etc/resolv.conf
#========================================================
```

Sla het bestand op en start de machine opnieuw op. De instellingen zouden nu moeten blijven bestaan. 

```
docker@boot2docker:~$ sudo reboot
```

SSH vervolgens weer de machine in en voer de volgende commando's uit om te kijken of de IP nummers geactiveerd werden.

```
docker@boot2docker:~$ ping 192.168.0.2
docker@boot2docker:~$ ping 192.168.0.10
```

### SSH into the data nodes

Herhaal de stappen, maar nu voor de 2 data nodes in het cluster. Wijs voor elke machine een statisch IP-adres toe op eth1.

data node1: 192.168.0.3
data node2: 192.168.0.4

#### Data node 1 profile script

```
#========================================================
#!/bin/sh
# kill dhcp client for eth1
if [ -f /var/run/udhcpc.eth1.pid ]; then
kill `cat /var/run/udhcpc.eth1.pid`
sleep 0.1
fi
# configure interface eth1
sudo ifconfig eth1 192.168.0.3 netmask 255.255.255.0 broadcast 192.168.0.255 up
sudo route add default gw 192.168.0.254
sudo echo nameserver 192.168.0.254 >> /etc/resolv.conf
#========================================================
```

#### Data node 2 profile script

```
#========================================================
#!/bin/sh
# kill dhcp client for eth1
if [ -f /var/run/udhcpc.eth1.pid ]; then
kill `cat /var/run/udhcpc.eth1.pid`
sleep 0.1
fi
# configure interface eth1
sudo ifconfig eth1 192.168.0.4 netmask 255.255.255.0 broadcast 192.168.0.255 up
sudo echo nameserver 192.168.0.254 >> /etc/resolv.conf
#========================================================
```
## MySQL Cluster Docker Images

We maken bij `morstead` gebruik van geoptimaliseerde MySQL Cluster Docker-images. Deze worden gemaakt en onderhouden door het MySQL-team bij Oracle. De beschikbare versies zijn:

* MySQL Cluster 7.5 (tag: 7.5)
* MySQL Cluster 7.6, de nieuwste GA-versie (tag: 7.6 of laatste)

Docker images worden bijgewerkt wanneer nieuwe MySQL Cluster-onderhoudsreleases en ontwikkelingsmijlpalen worden gepubliceerd. Houd er rekening mee dat alle MySQL Cluster Docker-images als experimenteel moeten worden beschouwd en `niet in productie` mogen worden gebruikt.

### Installeren volgens standaard configuratie

#### Docker Netwerk

Maak alvorens installatie op alle 3 de boot2docker nodes een docker netwerk voor het cluster aan.

```
docker@boot2docker:~$ docker network create cluster --subnet=192.168.0.0/16
```

#### Management Node

Daarna starten we de management node door op de management node machine de container op te starten.

```
docker run -d --net=cluster --name=management1 --ip=192.168.0.2 mysql/mysql-cluster ndb_mgmd
```

#### De twee data nodes

Op de overige 2 machines installeren we de 2 data nodes door op elke machine een data node container te starten.

```
docker@boot2docker:~$ docker run -d --net=cluster --name=ndb1 --ip=192.168.0.3 mysql/mysql-cluster ndbd
docker@boot2docker:~$ docker run -d --net=cluster --name=ndb2 --ip=192.168.0.4 mysql/mysql-cluster ndbd
```

#### Mysqld

Op de managment node installeren we de mysql daemon.

```
docker@boot2docker:~$  docker run -d --net=cluster --name=mysql1 --ip=192.168.0.10 -e MYSQL_RANDOM_ROOT_PASSWORD=true mysql/mysql-cluster mysqld
```

De server wordt geïnitialiseerd met een willekeurig wachtwoord dat moet worden gewijzigd, dus haal het op uit het logboek, log dan in en wijzig het wachtwoord. Als u een foutmelding krijgt met de melding «ERROR 2002 (HY000): Kan geen verbinding maken met lokale MySQL-server via socket», dan is de server nog niet klaar met initialiseren.

```
docker@boot2docker:~$ docker logs mysql1 2>&1 | grep PASSWORD
docker@boot2docker:~$ docker exec -it mysql1 mysql -uroot -p
docker@boot2docker:~$ ALTER USER 'root'@'localhost' IDENTIFIED BY 'MyNewPass';
```

### Interactief beheer

Start ten slotte een container met een interactieve beheerclient om te controleren of het cluster actief is.

```
docker@boot2docker:~$  docker run -it --net=cluster mysql/mysql-cluster ndb_mgm
```

Voer de opdracht SHOW uit om de clusterstatus te zien. Je zou het volgende moeten zien:

```
Starting ndb_mgm
-- NDB Cluster -- Management Client --
ndb_mgm> show
Connected to Management Server at: 192.168.0.2:1186
Cluster Configuration
---------------------
[ndbd(NDB)]    2 node(s)
id=2    @192.168.0.3  (mysql-5.7.18 ndb-7.6.2, Nodegroup: 0, *)
id=3    @192.168.0.4  (mysql-5.7.18 ndb-7.6.2, Nodegroup: 0)

[ndb_mgmd(MGM)]    1 node(s)
id=1    @192.168.0.2  (mysql-5.7.18 ndb-7.6.2)

[mysqld(API)]    1 node(s)
id=4    @192.168.0.10  (mysql-5.7.18 ndb-7.6.2)
```




