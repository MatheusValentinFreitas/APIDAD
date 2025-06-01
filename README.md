# APIDAD

Aluno: Matheus Antônio Valentin Freitas

Parte notiticação: A notificação esta dividida em duas tabelas principais uma de
notificação e uma de acesso a area de notificação sendo o OauthTokens a tabela
utilizada para acessar a notificação. O metodo usado é a pré geração de um token
na tabela de OauthTokens e com o token feito é usado para acesso a tabela de
notificação.
Além disso existe um job que executa de um em um minuto verificando se alguma
notificação chegou ao prazo e caso tenha chegado troca o status de 0 para 1.