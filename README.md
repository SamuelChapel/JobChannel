<h1>JobChannel : Solutions pour l’Insertion Professionnelle</h1>

## Introduction
Le service d'insertion de l'association AMIO gère les offres d'emploi pour les stages, l'alternance et les emplois. 

Ces offres proviennent de diverses sources et sont ensuite transmises par e-mail aux stagiaires. 

Pour améliorer la mise à disposition des offres d'emploi et impliquer davantage les stagiaires dans leurs recherches, 
nous allons développer une solution avec une architecture en couches.

## Architecture Globale
JobChannel adopte une architecture en couches pour séparer les responsabilités et faciliter la maintenance. 

Voici les applications de cette solution :

1. **Partie Serveur**:
    - Base de données relationnelle : Utilisation de **SQL Server** comme SGBDR pour stocker les données des offres d'emploi.
    - **API REST** avec le **Framework ASP.NET 6**. Cette API servira d'intermédiaire entre la base de données et les applications clientes.

2. **Partie Client**:
    - **Application Desktop pour les Conseillers d'Insertion**:
        - Application **WinForms** pour permettre aux conseillers d'insertion de gérer les offres d'emploi mises à disposition des stagiaires.
    - **Application Mobile pour les Stagiaires**:
        - Pour les stagiaires, application **UWP** (Universal Windows Platform) avec une architecture **MVVM**. Cette application permettra aux stagiaires de consulter les offres d'emplois.

## Tests
Les test ont étés éffectués tout au long du développement en utilisant des **tests unitaires** et des **tests d'intégration** avec le framework **xUnit**.
<br>
<p align="center">
  <h1>JobChannel Api</h2>
</p>
L’API JobChannel est une application ASP.NET 6 qui sert d’intermédiaire entre la base de données et les applications clientes. 
Elle permet de gérer les offres d’emploi et d’intégrer de nouvelles offres à partir des API de Pôle Emploi.

## Fonctionnalités
- CRUD pour les offres d’emploi :
  - Créer, lire, mettre à jour et supprimer des offres d’emploi.

- Intégration avec les API de Pôle Emploi :
  - Récupérer automatiquement de nouvelles offres d’emploi à intervalle régulier.
  - Mettre à jour la base de données avec ces offres pour qu’elles soient accessibles dans JobChannel.

## Architecture Multicouches 3 Tiers
L’application serveur JobChannel suit une architecture multicouches 3 tiers pour garantir la séparation des responsabilités et la maintenabilité :

![image](https://github.com/SamuelChapel/JobChannel/assets/86355019/a3c47402-41df-426a-b09d-a3b7d25a0c3a)

## Couche de Présentation (Api) :

Couche d’accès WebService de l’application qui permet la communication avec les applications clientes par l’intermédiaire de requêtes http

## Couche Métier (Bll) :

Contient la logique métier de l’application.
Gère les interactions avec la base de données et les API de Pôle Emploi.
Effectue les opérations CRUD sur les offres d’emploi.

## Couche d’Accès aux Données (Dal) :

Communique avec la base de données.
Stocke et récupère les informations sur les offres d’emploi.

![image](https://github.com/SamuelChapel/JobChannel/assets/86355019/068409b5-0c3f-4822-9ce4-a51b292d6ce1)
<p align="center">
  <em>Modèle Conceptuel de Données</em>
</p>
