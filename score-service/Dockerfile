# Use a imagem oficial do OpenJDK como base
FROM openjdk:17-jdk-slim

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos do projeto para o contêiner
COPY . /app

# Compile o projeto usando o Kotlin
RUN ./gradlew build

# Defina a variável de ambiente para a porta (pode ser 8080 ou 8081)
ARG PORT=8080
ENV PORT=${PORT}

# Exponha a porta que o serviço irá rodar
EXPOSE ${PORT}

# Execute o arquivo principal do Ktor
CMD ["java", "-jar", "build/libs/seu-jar-application.jar"]
