CREATE TABLE `pagamento` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipoPagamento` int(11) NOT NULL,
  `IdEmpresa` int(11) NOT NULL,
  `IdPessoa` int(11) NOT NULL,
  `Descricao` varchar(250) NOT NULL,
  `Valor` decimal(10,2) NOT NULL,
  `DataVencimento` datetime NOT NULL,
  `DataPagamento` datetime NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_pagamento_empresa_idx` (`IdEmpresa`),
  KEY `fk_pagamento_pessoa_idx` (`IdPessoa`),
  KEY `fk_pagamento_tipopagamento_idx` (`IdTipoPagamento`),
  CONSTRAINT `fk_pagamento_empresa` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pagamento_pessoa` FOREIGN KEY (`IdPessoa`) REFERENCES `pessoa` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_pagamento_tipopagamento` FOREIGN KEY (`IdTipoPagamento`) REFERENCES `tipopagamento` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;