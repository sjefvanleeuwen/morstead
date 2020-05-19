
<!-- TOC depthfrom:1 orderedlist:true -->

- [1. Morstead Design System](#1-morstead-design-system)
    - [1.1. Stateless Grain](#11-stateless-grain)
    - [1.2. Stateless Grain (re-entrant)](#12-stateless-grain-re-entrant)
    - [1.3. Statefull Grain](#13-statefull-grain)
        - [1.3.1. Observer Grain](#131-observer-grain)
- [2. Atomic Design](#2-atomic-design)
    - [2.1. Atoms](#21-atoms)
    - [2.2. Molecules](#22-molecules)
        - [2.2.1. Consensus publication](#221-consensus-publication)
    - [2.3. Organisms](#23-organisms)

<!-- /TOC -->


# 1. Morstead Design System
*under construction* 

Om morstead te kunnen modeleren in de solution architectuur is er een notatiesysteem opgesteld:


## 1.1. Stateless Grain

Stateless Worker grains bieden een eenvoudige manier om een automatisch beheerde pool van 
grain activaties te creeren die automatisch opschaalt op basis van de werkelijke belasting.

<img src="../images/grain-worker.svg" width="96"></img>

## 1.2. Stateless Grain (re-entrant)

Stateless betekent overigens niet dat een worker grain een status kan hebben en uitsluitend
is beperkt tot het uitvoeren van functies. Net als elk ander type grain kan deze een gewenste
status bewaren in het geheugen (re-entrant) (maar persisteert de staat niet op een medium voor langdurig gebruik)
Er is echter geen eenvoudig mechanisme om de de status bij te houden en verschillende
activeringen te coördineren.

<img src="../images/grain-worker-re-entrant.svg" width="96"></img>

## 1.3. Statefull Grain
<img src="../images/grain-statefull.svg" width="96"></img>

### 1.3.1. Observer Grain
<img src="../images/grain-observer.svg" width="96"></img>

# 2. Atomic Design

## 2.1. Atoms

## 2.2. Molecules

### 2.2.1. Consensus publication

[Flow](./molecules/notation.html?consensus.json)

## 2.3. Organisms

