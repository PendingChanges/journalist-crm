MATCH (n) DETACH DELETE n;
CREATE (n:Idea {Id: '1', Name: 'Breakdance', Description: 'Article sur le breakdance'});

CREATE (n:Client {Id: '1', Name: 'Phosphore'});