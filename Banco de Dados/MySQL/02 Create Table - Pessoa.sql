CREATE TABLE `pessoa` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` nvarchar(100) NOT NULL,
  `ImagemPerfil` text NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;