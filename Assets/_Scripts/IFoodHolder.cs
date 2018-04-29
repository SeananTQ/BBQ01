using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFoodHolder   {

    Transform GetFoodPositionTransform();

    bool TakeFood(FoodSprite food);

     void FoodAway();

}
