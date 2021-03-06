﻿using System;
using System.Collections.Generic;
using Tinifier.Core.Models.API;
using Tinifier.Core.Models.Db;
using Tinifier.Core.Repository.Repository;
using Tinifier.Core.Services.Interfaces;

namespace Tinifier.Core.Services.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly THistoryRepository _historyRepository;

        public HistoryService()
        {
            _historyRepository = new THistoryRepository();  
        }

        public void CreateResponseHistoryItem(int timageId, TinyResponse responseItem)
        {
            var newItem = new TinyPNGResponseHistory
            {
                OccuredAt = DateTime.UtcNow,
                IsOptimized = responseItem.Output.IsOptimized,
                ImageId = timageId,
                Error = responseItem.Output.Error,
                Ratio = responseItem.Output.Ratio,
                OptimizedSize = responseItem.Output.Size
            };

            if (responseItem.Input != null)
            {
                newItem.OriginSize = responseItem.Input.Size;
                newItem.Error = string.Empty;
            }

            _historyRepository.Create(newItem);
        }

        public TinyPNGResponseHistory GetHistoryForImage(int timageId)
        {
            var history = _historyRepository.GetByKey(timageId);

            if(history != null)
            {
                history.OccuredAt = new DateTime(history.OccuredAt.Year, history.OccuredAt.Month,
                                history.OccuredAt.Day, history.OccuredAt.Hour, history.OccuredAt.Minute, history.OccuredAt.Second);
            }
            
            return history;
        }

        public List<TImage> CheckImageHistory(IEnumerable<TImage> images)
        {
            var imagesList = new List<TImage>();

            foreach (var image in images)
            {
                var imageHistory = _historyRepository.GetByKey(image.Id);

                if (imageHistory != null)
                {
                    if (!imageHistory.IsOptimized)
                    {
                        imagesList.Add(image);
                    }
                }
                else
                {
                    imagesList.Add(image);
                }
            }

            return imagesList;
        }
    }
}
