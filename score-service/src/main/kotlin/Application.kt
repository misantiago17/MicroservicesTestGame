package com.example

import io.ktor.http.HttpStatusCode
import io.ktor.server.engine.*
import io.ktor.server.netty.*
import io.ktor.server.application.*
import io.ktor.server.response.*
import io.ktor.server.request.*
import io.ktor.server.routing.*
import io.ktor.server.plugins.contentnegotiation.*
import kotlinx.serialization.Serializable

import io.ktor.serialization.kotlinx.json.*
import kotlinx.serialization.json.Json


fun main() {
    embeddedServer(Netty, port = 8080, host = "0.0.0.0") {
        install(ContentNegotiation) {
            json(Json {
                prettyPrint = true
                isLenient = true
                ignoreUnknownKeys = true
            })
        }
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
    }.start(wait = true)
}

@Serializable
data class StatusResponse(val message: String)

@Serializable
data class Score (
    val player: String,
    val points: Int
)