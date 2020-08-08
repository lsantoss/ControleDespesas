use controledespesas;

CREATE TABLE `usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` nvarchar(50) NOT NULL,
  `Senha` nvarchar(15) NOT NULL,
  `Privilegio` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;