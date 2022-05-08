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
            let content = SeqHeml(message.content);
            let date = message.date;
            let messageHelpId = message.messageHelpId;


            if (sender == senderName) {

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

    //отправка удаление сообщение у всех
    document.querySelectorAll('.message_delete').forEach(function (el) {

        el.addEventListener("click", function (e) {
            var messageId = e.currentTarget.id.split("_")[0];

            let sendmess = new DeleteMessage(sender, recipient, parseInt(id), parseInt(messageId), false);

            hubConnection.invoke("ToDelete", sendmess);

        });
        
    });

    hubConnection.on('FromUpdate', function (message) {

        var helpid = message.messageHelpId;

        var text = SeqHeml(message.text);

        document.getElementById(`${helpid}_message_text`).innerText = text
    });

    document.querySelectorAll('.message_update').forEach(function (el) {

        el.addEventListener("click", function (e) {
            var messageId = e.currentTarget.id.split("_")[0];

            var text = document.getElementById(`${messageId}_message_text`).innerText;

            let sendmess = new UpdateMessage(sender, recipient, parseInt(id), parseInt(messageId), text);

            hubConnection.invoke("ToUpdate", sendmess);

        });

    });

    hubConnection.start();
}

function StartGroupChat(groupName, sender, colorUser) {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/groupchat")
        .build();

    let userName = '';

    // получение сообщения от сервера
    hubConnection.on('Receive', function (message) {
        let senderName = message.senderName;
        let content = SeqHeml(message.content);
        let date = message.date;
        let nams = message.recipientName;

        if (sender == senderName) {

            var html = `<div class="message__box_holder" >
                <div class="message__box mid_${colorUser} mid_${colorUser}_border
                     mid_${colorUser}_border_triangle">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                            ${nams}
                        </div>
                    <div class="messages__block_m_l_t">
                        ${date}
                    </div>
                </div>
            </div>`;
        }
        else {
            var html = `<div class="message__box_holder message__partner" ">
                <div class="message__box mid_${colorUser} mid_${colorUser}_border message__partner_box
                     mid_${colorUser}_border_triangle_partner">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                        ${nams}
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
    });

    // отправка сообщения на сервер
    document.getElementById("sendBtn").addEventListener("click", function (e) {
        let message = document.getElementById("message").value;

        let sendmess = new GroupMessage(groupName, sender, '', message, '');

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

    //отправка удаление сообщение у всех
    document.querySelectorAll('.message_delete').forEach(function (el) {

        el.addEventListener("click", function (e) {
            var messageId = e.currentTarget.id.split("_")[0];

            let sendmess = new DeleteGroupMessage(groupName, parseInt(messageId), false);

            hubConnection.invoke("ToDelete", sendmess);

        });

    });

    hubConnection.start();
}

function StartSecretChat(id, sender, recipient, colorAut, colorUser) {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/secretchat")
        .build();

    let userName = '';

    // получение сообщения от сервера
    hubConnection.on('Receive', function (message) {
        let jsid = message.id;
        if (jsid == id) {
            let senderName = message.senderName;
            let content = SeqHeml(message.content);
            let date = message.date;
            let messageHelpId = message.messageHelpId;


            if (sender == senderName) {

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


    hubConnection.start();
}

function StartClashChat(id, sender, group, color, is1) {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/clashchat")
        .build();

    // получение сообщения от сервера
    hubConnection.on('Receive', function (message) {
        let senderName = message.senderName;
        let content = SeqHeml(message.text);
        let date = message.date;
        let nams = message.recipientName;
        let is1 = message.is1;
        let color = message.color;

        if (is1 == "true") {

            var html = `<div class="message__box_holder" >
                <div class="message__box mid_${color} mid_${color}_border
                     mid_${color}_border_triangle">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                            ${nams}
                        </div>
                    <div class="messages__block_m_l_t">
                        ${date}
                    </div>
                </div>
            </div>`;
        }
        else {
            var html = `<div class="message__box_holder message__partner" ">
                <div class="message__box mid_${color} mid_${color}_border message__partner_box
                     mid_${color}_border_triangle_partner">
                    <div>
                        ${content}
                    </div>
                    <div class="messages__block_m_l_t">
                        ${nams}
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
    });

    if (document.getElementById("sendBtn") != null) {
        document.getElementById("sendBtn").addEventListener("click", function (e) {
            let message = document.getElementById("message").value;

            let sendmess = new ClashMessage(group, parseInt(id), sender, '', '', message, is1, color);

            //let sendmess = "sss";

            hubConnection.invoke("Send", sendmess);
            /*hubConnection.invoke("Send", '2');*/

            document.getElementById("message").value = "";
        });
    }

    hubConnection.on('ReceiveLike', function (message) {

        if (message.isAddLike) {
            if (message.is1) {
                var num = parseInt(document.getElementById("1likeAll").innerText) + 1;
                document.getElementById("1likeAll").innerText = num;
            }
            else {

                var num = parseInt(document.getElementById("2likeAll").innerText) + 1;
                document.getElementById("2likeAll").innerText = num;
            }
        }
        else {
            if (message.is1) {
                var num = parseInt(document.getElementById("1likeAll").innerText) - 1;
                document.getElementById("1likeAll").innerText = num;
            }
            else {

                var num = parseInt(document.getElementById("2likeAll").innerText) - 1;
                document.getElementById("2likeAll").innerText = num;
            }
        }
    });

    document.getElementById("1Like1").addEventListener("click", function (e) {

        var likeMess = new ClashLike(true, parseInt(id), sender, true);

        hubConnection.invoke("SetLike", likeMess);

        document.getElementById("1Like1").classList.add('heart_none');
        document.getElementById("1Like2").classList.remove('heart_none');
    });

    document.getElementById("1Like2").addEventListener("click", function (e) {

        var likeMess = new ClashLike(true, parseInt(id), sender, false);

        hubConnection.invoke("SetLike", likeMess);

        document.getElementById("1Like2").classList.add('heart_none');
        document.getElementById("1Like1").classList.remove('heart_none');
    });

    document.getElementById("2Like1").addEventListener("click", function (e) {

        var likeMess = new ClashLike(false, parseInt(id), sender, true);

        hubConnection.invoke("SetLike", likeMess);

        document.getElementById("2Like1").classList.add('heart_none');
        document.getElementById("2Like2").classList.remove('heart_none');
    });

    document.getElementById("2Like2").addEventListener("click", function (e) {

        var likeMess = new ClashLike(false, parseInt(id), sender, false);

        hubConnection.invoke("SetLike", likeMess);

        document.getElementById("2Like2").classList.add('heart_none');
        document.getElementById("2Like1").classList.remove('heart_none');
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

class UpdateMessage {
    constructor(senderName, recipientName, chatId, messageHelpId, text) {
        this.SenderName = senderName;
        this.RecipientName = recipientName;
        this.ChatId = chatId;
        this.MessageHelpId = messageHelpId;
        this.Text = text;
    }
}

class GroupMessage {
    constructor(groupName, senderName, recipientName, text, when) {
        this.SenderName = senderName;
        this.RecipientName = recipientName;
        this.Content = text;
        this.Date = when;
        this.GroupName = groupName;
    }
}

class DeleteGroupMessage {
    constructor(groupName, messageHelpId, isSuccess) {
        this.GroupName = groupName;
        this.MessageHelpId = messageHelpId;
        this.IsSuccess = isSuccess;
    }
}

class ClashMessage {
    constructor(groupName, clashId, sender, recipientName, date, text, is1, color) {
        this.GroupName = groupName;
        this.ClashId = clashId;
        this.Sender = sender;
        this.RecipientName = recipientName;
        this.Date = date;
        this.Text = text;
        this.Is1 = is1;
        this.Color = color;
    }
}

class ClashLike {
    constructor(is1, clashId, sender, isAddLike) {
        this.ClashId = clashId;
        this.Sender = sender;
        this.Is1 = is1;
        this.IsAddLike = isAddLike;
    }
}