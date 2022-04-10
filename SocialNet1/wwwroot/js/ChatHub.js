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
            let messageHelpId = message.messageHelpId;

            //var lastElem = document.getElementById("chatroom").lastChild;

            if (sender == senderName) {
            //    var html = `<div class="message__box_holder" id="${messageHelpId}_message">
            //    <div class="message__box mid_${colorAut} mid_${colorAut}_border
            //         mid_${colorAut}_border_triangle">
            //        <div>
            //            ${content}
            //        </div>
            //        <div class="messages__block_m_l_t">
            //                <button id="${messageHelpId}_message_delete" class="message_delete">
            //                    <i class="fa fa-trash-o color_dark_dark_${colorAut} fa-1x" aria-hidden="true"></i>
            //                </button>
            //                <button id="${messageHelpId}_message_update" class="">
            //                    <i class="fa fa-pencil fa-1x color_dark_dark_${colorAut}" aria-hidden="true"></i>
            //                </button>
            //            ${date}
            //        </div>
            //    </div>
            //</div>`;

                var html = `<div class="message__box_holder" id="${messageHelpId}_message">
                <div class="message__box mid_${colorAut} mid_${colorAut}_border
                     mid_${colorAut}_border_triangle">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                        ${date}
                    </div>
                </div>
            </div>`;
            }
            else {
                var html = `<div class="message__box_holder message__partner" id="${messageHelpId}_message">
                <div class="message__box mid_${colorUser} mid_${colorUser}_border message__partner_box
                     mid_${colorUser}_border_triangle_partner">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                        ${date}
                    </div>
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

    //удаление сообщения от сервера
    hubConnection.on('FromDelete', function (message) {
        let result = message.isSuccess;
        if (result) {

            var messageElement = document.getElementById(`${message.messageHelpId}_message`);
            messageElement.remove();
        }
        else {
            alert("Сообщение не удалилось(")
        }
    });

    let deletes = document.querySelectorAll('.message_delete');
    
    //отправка удаление сообщение у всех
    deletes.forEach(function (el) {

        el.addEventListener("click", function (e) {
            var messageId = e.currentTarget.id.split("_")[0];

            let sendmess = new DeleteMessage(sender, recipient, parseInt(id), parseInt(messageId), false);

            hubConnection.invoke("ToDelete", sendmess);

        });
        
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

class DeleteMessage {
    constructor(senderName, recipientName, chatId, messageHelpId, isSuccess) {
        this.SenderName = senderName;
        this.RecipientName = recipientName;
        this.ChatId = chatId;
        this.MessageHelpId = messageHelpId;
        this.IsSuccess = isSuccess;
    }
}
