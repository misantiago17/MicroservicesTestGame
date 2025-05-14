package com.example

import com.fasterxml.jackson.databind.*
import io.ktor.http.HttpStatusCode
import io.ktor.serialization.jackson.*
import io.ktor.server.application.*
import io.ktor.server.plugins.calllogging.*
import io.ktor.server.plugins.contentnegotiation.*
import io.ktor.server.request.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import org.slf4j.event.*

fun Application.configureRouting() {
    routing {
        get("/") {
            call.respondText("Servidor do jogo está online!")
        }

        get("/status") {
            call.respond(StatusResponse("Tudo certo por aqui!"))
        }

        post("/score"){
            val score = call.receive<Score>()
            println("Jogador: ${score.player} fez ${score.points} pontos!")
            call.respond(HttpStatusCode.OK, "Pontuação recebida!")
        }
    }
}
