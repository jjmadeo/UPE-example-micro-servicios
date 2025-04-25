FROM mariadb:latest
RUN apt-get update && apt-get install -y mariadb-client
# Copia scripts de inicialización al lugar que MariaDB usa por defecto
COPY init /docker-entrypoint-initdb.d
