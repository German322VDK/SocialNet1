function StartChat(id, sender, recipient, colorAut, colorUser) {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    let userName = '';
    // получение сообщения от сервера
    hubConnection.on('Receive', function (message) {
        let jsid = message.id;
        if (jsid == id) {
            let senderName = message.senderName;
            let content = message.content;
            let date = message.date;
            // создаем элемент <b> для имени пользователя
            //let userNameElem = document.createElement("b");
            //userNameElem.appendChild(document.createTextNode(userName + ' : ' + date + ': '));

            //// создает элемент <p> для сообщения пользователя
            //let elem = document.createElement("p");
            //elem.appendChild(userNameElem);
            //elem.appendChild(document.createTextNode(content));

            //var lastElem = document.getElementById("chatroom").lastChild;

            if (sender == senderName) {
                var html = `<div class="message__box_holder">
                <div class="message__box mid_${colorAut} mid_${colorAut}_border
                     mid_${colorAut}_border_triangle">
                    ${content}
                    <div class="messages__block_m_l_t">${date}</div>
                </div>
            </div>`;
            }
            else {
                var html = `<div class="message__box_holder message__partner">
                <div class="message__box mid_${colorUser} mid_${colorUser}_border message__partner_box
                     mid_${colorUser}_border_triangle_partner">
                    ${content}
                    <div class="messages__block_m_l_t">${date}</div>
                </div>
            </div>`;
            }


            $("#chatroom").append(html);

            document.getElementById("lastTime").innerText = date;

            window.scrollTo(0, window.innerWidth);

            //document.getElementById("chatroom").appendChild(elem);
        }
    });

    // отправка сообщения на сервер
    document.getElementById("sendBtn").addEventListener("click", function (e) {
        let message = document.getElementById("message").value;

        let sendmess = new Message(sender, recipient, message, '', parseInt(id));

        hubConnection.invoke("Send", sendmess);
        /*hubConnection.invoke("Send", '2');*/

        document.getElementById("message").value = "";
    });

    hubConnection.start();
}

class Message {
    constructor(senderName, recipientName, text, when, id) {
        this.SenderName = senderName;
        this.RecipientName = recipientName;
        this.Content = text;
        this.Date = when;
        this.Id = id;
    }
}
