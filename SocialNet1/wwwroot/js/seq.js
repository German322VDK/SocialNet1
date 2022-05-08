function SeqHeml (a) {
	return a.replace(/[<'&">]/g, function (symb) {
		return {
			'&': '&amp;',
			'\'': '&#039;',
			'\"': '&quot;',
			'<': '&lt;',
			'>': '&gt;'
		}[symb];
	});
};